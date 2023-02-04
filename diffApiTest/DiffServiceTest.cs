namespace diffApiTest;

[TestClass]
public class DiffServiceTest
{
    // DataRow doesn't support enumeration values, known bug since 2018 :( 
    [TestMethod]
    [DataRow(0)] // IRepository.Side.Left
    [DataRow(1)] // IRepository.Side.Right
    public void StoreInput(int s)
    {
        var side = (Side)s;

        var diffService = new DiffService(new RepositoryMock());
        var inputRecord = new InputRecord("1", "test-input");
        
        // return is void, so testing just for possible sideeffects
        diffService.StoreInputData(side, inputRecord);
    }

    [TestMethod]
    [DataRow(0)] // IRepository.Side.Left
    [DataRow(1)] // IRepository.Side.Right
    [ExpectedException(typeof(DuplicateInputException))]
    public void ConflictIsThrownWhenDuplicateInputIsStored(int s)
    {
        var side = (Side)s;
        
        var diffService = new DiffService(new RepositoryDuplicateMock());
        var inputRecord = new InputRecord("1", "test-input");
        
        diffService.StoreInputData(side, inputRecord);
    }

    [TestMethod]
    public void ReturnNullIfLeftInputIsMissing()
    {
        var diffService = new DiffService(new RepositoryMissingLeftMock());
        
        var diffResult = diffService.GetDiff("1");

        Assert.IsNull(diffResult);
    }        

    [TestMethod]
    public void ReturnNullIfRightInputIsMissing()
    {
        var diffService = new DiffService(new RepositoryMissingRightMock());
        
        var diffResult = diffService.GetDiff("1");

        Assert.IsNull(diffResult);
    } 

    [TestMethod]
    public void ReturnSameInputsResult()
    {
        var diffService = new DiffService(new RepositorySameInputsMock());
        
        var diffResult = diffService.GetDiff("1");

        Assert.IsNotNull(diffResult);
        Assert.AreEqual("1", diffResult.id);
        Assert.AreEqual(0, diffResult.differences.Count());
        Assert.AreEqual("inputs were equal", diffResult.description);
    }

    [TestMethod]
    public void ReturnDifferentLengthInputsResult()
    {
        var diffService = new DiffService(new RepositoryDifferentLengthInputsMock());
        
        var diffResult = diffService.GetDiff("1");

        Assert.IsNotNull(diffResult);
        Assert.AreEqual("1", diffResult.id);
        Assert.AreEqual(0, diffResult.differences.Count());
        Assert.AreEqual("inputs are of different size", diffResult.description);
    }  

    [TestMethod]
    public void ReturnDifference()
    {
        var diffService = new DiffService(new RepositorySameLengthInputsMock());
        
        var diffResult = diffService.GetDiff("1");

        Assert.IsNotNull(diffResult);
        Assert.AreEqual("1", diffResult.id);
        Assert.AreEqual(7, diffResult.differences.Count());
        Assert.AreEqual(string.Empty, diffResult.description);

        var expectedDiff = GetExpectedDiff();

        CollectionAssert.AreEqual(expectedDiff, diffResult.differences.ToList());
    }

    // generates expected diffs for inuts returned in class RepositorySameLengthInputsMock
    List<Diff> GetExpectedDiff()
    {
        var expectedDiff = new List<Diff>();

        expectedDiff.Add(new Diff(13, 'n', 'k'));
        expectedDiff.Add(new Diff(14, 'N', 'h'));

        expectedDiff.Add(new Diff(20, 'd', 'W'));
        expectedDiff.Add(new Diff(21, 'm', 'G'));
        expectedDiff.Add(new Diff(28, 'd', 'M'));
        expectedDiff.Add(new Diff(29, 'G', 'W'));
        expectedDiff.Add(new Diff(31, 'g', 'r'));

        return expectedDiff;
    }     
}