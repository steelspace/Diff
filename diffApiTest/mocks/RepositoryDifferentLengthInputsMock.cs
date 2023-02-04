public class RepositoryDifferentLengthInputsMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Left)
        {
            return new InputRecord(id, Side.Left, "TEST-INPUT");
        }

        return new InputRecord(id, Side.Right, "LONGER-TEST-INPUT");
    }

    public void StoreInput(InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
