public interface IRepository
{
    public enum Side
    {
        Left,
        Right
    }

    void StoreInput(Side side, InputRecord inputRecord);

    InputRecord? LoadInput(Side side, string id);
}