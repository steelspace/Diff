public class DiffService : IDiffService
{
    private readonly IRepository repository;

    public DiffService(IRepository repository) => this.repository = repository;

    public void StoreInputData(Side side, InputRecord inputRecord)
    {
        repository.StoreInput(side, inputRecord);
    }
}