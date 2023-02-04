namespace diffApiTest;

[TestClass]
public class MemoryRepositoryTest
{
    // DataRow doesn't support enumeration values, known bug since 2018 :( 
    [TestMethod]
    [DataRow(0)] // IRepository.Side.Left
    [DataRow(1)] // IRepository.Side.Right
    public void InputIsStored(int s)
    {
        var side = (Side)s;

        var memoryRepository = new MemoryRepository();
        var inputRecord = new InputRecord("1", side, "test-input");
        
        memoryRepository.StoreInput(inputRecord);

        var savedInputRecord = memoryRepository.LoadInput(side, "1");

        var expectedRecord = new InputRecord("1", side, "test-input");
        Assert.AreEqual(expectedRecord, savedInputRecord);
    }

    [TestMethod]
    [DataRow(0)] // IRepository.Side.Left
    [DataRow(1)] // IRepository.Side.Right
    [ExpectedException(typeof(ArgumentException))]
    public void ConflictIsThrownWhenDuplicateInputIsStored(int s)
    {
        var side = (Side)s;
        
        var memoryRepository = new MemoryRepository();
        var inputRecord = new InputRecord("1", Side.Left, "test-input");
        
        memoryRepository.StoreInput(inputRecord);
        memoryRepository.StoreInput(inputRecord);
    }    
}