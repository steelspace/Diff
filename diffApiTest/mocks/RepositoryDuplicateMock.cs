public class RepositoryDuplicateMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        throw new NotImplementedException();
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        // when a duplicate record is found, throw ArgumentException
        throw new ArgumentException();
    }
}
