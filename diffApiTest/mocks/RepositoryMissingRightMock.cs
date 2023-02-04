public class RepositoryMissingRightMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Right)
        {
            return null;
        }

        return new InputRecord(id, "TEST-INPUT");
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
