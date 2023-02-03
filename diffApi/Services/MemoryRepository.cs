public class MemoryRepository : IRepository
{
    readonly Dictionary<string, string> inputs = new Dictionary<string, string>();

    string GetKey(Side side, string id)
    {
        return $"{side}/id";
    }

    public void StoreInput(Side side, InputRecord inputRecord)
    {
        var key = GetKey(side, inputRecord.id);

        lock (inputs)
        {
            if (inputs.ContainsKey(key))
            {
                throw new DuplicateInputException();
            }

            inputs.Add(key, inputRecord.input);
        }
    }

    public InputRecord? LoadInput(Side side, string id)
    {
        var key = GetKey(side, id);

        if (!inputs.TryGetValue(key, out string? input))
        {
            return null;
        }

        return new InputRecord(id, input);       
    }
}