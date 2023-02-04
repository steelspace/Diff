public interface IDiffService
{
    void StoreInputData(InputRecord inputRecord);

    DiffResult? GetDiff(string id);
}