public class RepositoryDifferentLengthInputsMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Left)
        {
            return new InputRecord(id, "TEST-INPUT");
        }

        return new InputRecord(id, "LONGER-TEST-INPUT");
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
