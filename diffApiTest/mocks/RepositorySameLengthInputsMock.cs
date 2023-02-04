public class RepositorySameLengthInputsMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Left)
        {
            return new InputRecord(id, Side.Left, "eyJpbnB1dCI6InNvbWUgdmFsdWUgdG8gYmUgY29tcGFyZWQifQ==");
        }

        return new InputRecord(id, Side.Right, "eyJpbnB1dCI6IkhvbWUgWGFsdWUgMW8rYmUgY29tcGFyZWQifQ==");
    }

    public void StoreInput(InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
