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
        var side = (IRepository.Side)s;

        var memoryRepository = new MemoryRepository();
        var inputRecord = new InputRecord("1", "test-input");
        
        memoryRepository.StoreInput(side, inputRecord);

        var savedInputRecord = memoryRepository.LoadInput(side, "1");

        Assert.AreEqual(inputRecord, savedInputRecord);
    }

    [TestMethod]
    [DataRow(0)] // IRepository.Side.Left
    [DataRow(1)] // IRepository.Side.Right
    [ExpectedException(typeof(DuplicateInputException))]
    public void ConflictIsThrownWhenDuplicateInputIsStored(int s)
    {
        var side = (IRepository.Side)s;
        
        var memoryRepository = new MemoryRepository();
        var inputRecord = new InputRecord("1", "test-input");
        
        memoryRepository.StoreInput(side, inputRecord);
        memoryRepository.StoreInput(side, inputRecord);
    }    
}