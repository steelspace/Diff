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
}