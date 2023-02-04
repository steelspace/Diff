public class RepositorySameInputsMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        return new InputRecord(id, "TEST-INPUT");
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
