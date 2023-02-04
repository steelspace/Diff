public interface IDiffService
{
    void StoreInputData(Side side, InputRecord inputRecord);

    DiffResult? GetDiff(string id);
}