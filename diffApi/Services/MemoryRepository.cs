public class MemoryRepository : IRepository
{
    readonly Dictionary<string, string> LeftInputs = new Dictionary<string, string>();
    readonly Dictionary<string, string> RightInputs = new Dictionary<string, string>();

    public void StoreInput(IRepository.Side side, InputRecord inputRecord)
    {
        switch (side)
        {
            case IRepository.Side.Left:
            {
                this.Store(LeftInputs, inputRecord);
                break;
            }

            case IRepository.Side.Right:
            {
                this.Store(RightInputs, inputRecord);
                break;
            }

            default:
            {
                throw new InvalidOperationException("Unsupported 'Side'");
            }
        }
    }

    public InputRecord? LoadInput(string id, IRepository.Side side)
    {
       switch (side)
        {
            case IRepository.Side.Left:
            {
                return this.Load(LeftInputs, id);
            }

            case IRepository.Side.Right:
            {
                return this.Load(RightInputs, id);
            }

            default:
            {
                throw new InvalidOperationException("Unsupported 'Side'");
            }
        }        
    }

    private InputRecord? Load(Dictionary<string, string> inputs, string id)
    {
        if (!inputs.TryGetValue(id, out string? input))
        {
            return null;
        }

        return new InputRecord(id, input);
    }

    private void Store(Dictionary<string, string> inputs, InputRecord inputRecord)
    {
        lock (inputs)
        {
            if (inputs.ContainsKey(inputRecord.id))
            {
                throw new DuplicateInputException();
            }

            inputs.Add(inputRecord.id, inputRecord.input);
        }
    }
}