namespace diffApiTest;

[TestClass]
public class MemoryRepositoryTest
{
    [TestMethod]
    public void LeftSideIsStored()
    {
        var memoryRepository = new MemoryRepository();
        var inputRecord = new InputRecord("1", "test-input");
        
        memoryRepository.StoreInput(IRepository.Side.Left, inputRecord);
    }
}