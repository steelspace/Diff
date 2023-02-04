public class RepositoryMissingRightMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Right)
        {
            return null;
        }

        return new InputRecord(id, Side.Left, "TEST-INPUT");
    }

    public void StoreInput(InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
