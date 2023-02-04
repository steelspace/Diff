public class DiffService : IDiffService
{
    private readonly IRepository repository;

    public DiffService(IRepository repository) => this.repository = repository;

    public DiffResult? GetDiff(string id)
    {
        var leftInput = repository.LoadInput(Side.Left, id);
        var rightInput = repository.LoadInput(Side.Right, id);

        if (leftInput is null || rightInput is null)
        {
            return null;
        }

        var differences = new List<Diff>();
        var diffResult = new DiffResult(id, "XXX", differences);

        return diffResult;
    }

    public void StoreInputData(Side side, InputRecord inputRecord)
    {
        try
        {
            repository.StoreInput(side, inputRecord);
        }

        catch (ArgumentException)
        {
            // key already exists, throw a duplicate exception
            throw new DuplicateInputException();
        }
    }
}