using diffApi.Controllers;

namespace diffApiTest;

[TestClass]
public class DiffControllerTest
{
    [TestMethod]
    public void ConflictIsThrownWhenDuplicateInputIsStored(int s)
    {
        var side = (Side)s;
        
        var memoryRepository = new DiffController();
        var inputRecord = new InputRecord("1", "test-input");
        
        memoryRepository.StoreInput(side, inputRecord);
        memoryRepository.StoreInput(side, inputRecord);
    }    
}