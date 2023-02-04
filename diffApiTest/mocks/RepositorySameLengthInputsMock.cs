public class RepositorySameLengthInputsMock : IRepository
{
    public InputRecord? LoadInput(Side side, string id)
    {
        if (side == Side.Left)
        {
            return new InputRecord(id, "eyJpbnB1dCI6InNvbWUgdmFsdWUgdG8gYmUgY29tcGFyZWQifQ==");
        }

        return new InputRecord(id, "eyJpbnB1dCI6IkhvbWUgWGFsdWUgMW8rYmUgY29tcGFyZWQifQ==");
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        throw new NotImplementedException();
    }
}
