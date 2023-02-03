public interface IRepository
{
    void StoreInput(Side side, InputRecord inputRecord);

    InputRecord? LoadInput(Side side, string id);
}