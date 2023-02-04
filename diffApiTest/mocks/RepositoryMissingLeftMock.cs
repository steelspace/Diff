public class RepositoryMissingLeftMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Left)
        {
            return null;
        }

        return new InputRecord(id, Side.Right, "TEST-INPUT");
    }

    public void StoreInput(InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
