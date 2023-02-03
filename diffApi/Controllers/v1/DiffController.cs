using Microsoft.AspNetCore.Mvc;
namespace diffApi.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DiffController : ControllerBase
{
    private readonly ILogger<DiffController> logger;
    private readonly IDiffService diffService;

    public DiffController(ILogger<DiffController> logger, IDiffService diffService)
    {
        this.logger = logger;
        this.diffService = diffService;
    }

    [HttpGet]
    [Route("{id}")]
    [MapToApiVersion("1.0")]
    public string Diff(string id)
    {
        return id;
    }

    [HttpPost]
    [Route("{id}/left")]
    [MapToApiVersion("1.0")]
    public IActionResult Left(string id, [FromBody]Input input)
    {
        try
        {
            var inputRecord = new InputRecord(id, input.input);
            diffService.StoreLeftInputData(inputRecord);
            return Ok();
        }

        catch (DuplicateInputException)
        {
            return Conflict();
        }
    }

    [HttpPost]
    [Route("{id}/right")]
    [MapToApiVersion("1.0")]
    public IActionResult Right(string id, [FromBody]Input input)
    {
        try
        {
            var inputRecord = new InputRecord(id, input.input);
            diffService.StoreRightInputData(inputRecord);
            return Ok();
        }

        catch (DuplicateInputException)
        {
            return Conflict();
        }
    }
}
