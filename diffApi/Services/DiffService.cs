public class DiffService : IDiffService
{
    private readonly IRepository repository;

    public DiffService(IRepository repository) => this.repository = repository;

    public void StoreLeftInputData(InputRecord inputRecord)
    {
        repository.StoreInput(IRepository.Side.Left, inputRecord);
    }

    public void StoreRightInputData(InputRecord inputRecord)
    {
        repository.StoreInput(IRepository.Side.Right, inputRecord);
    }
}