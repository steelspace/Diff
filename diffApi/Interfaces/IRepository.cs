public interface IRepository
{
    void StoreInput(InputRecord inputRecord);

    InputRecord? LoadInput(Side side, string id);
}