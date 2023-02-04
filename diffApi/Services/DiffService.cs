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

        if (leftInput.input == rightInput.input)
        {
            return new DiffResult(id, "inputs were equal", differences);
        }

        if (leftInput.input.Length != rightInput.input.Length)
        {
            return new DiffResult(id, "inputs are of different size", differences);
        }

        var diffs = GetDiff(leftInput.input, rightInput.input);

        return new DiffResult(id, string.Empty, diffs);
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

    List<Diff> GetDiff(string leftInput, string rightInput)
    {
        var diffs = new List<Diff>();

        if (leftInput.Length != rightInput.Length)
        {
            throw new ArgumentException("Both inputs musthave the same lenght for diffing");
        }

        for (int i = 0; i < leftInput.Length; i++)
        {
            char leftCharacter = leftInput[i];
            char rightCharacter = rightInput[i];

            if (leftCharacter != rightCharacter)
            {
                var diff = new Diff(i, leftCharacter, rightCharacter);
                diffs.Add(diff);
            }
        }

        return diffs;
    }
}