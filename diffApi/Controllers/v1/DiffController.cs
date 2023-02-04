using Microsoft.AspNetCore.Mvc;
namespace diffApi.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DiffController : ControllerBase
{
    private readonly IDiffService diffService;

    public DiffController(IDiffService diffService)
    {
        this.diffService = diffService;
    }

    [HttpGet]
    [Route("{id}")]
    [MapToApiVersion("1.0")]
    public IActionResult Diff(string id)
    {
        var diffResult = diffService.GetDiff(id);

        if (diffResult is null)
        {
            return NotFound();
        }

        return Ok(diffResult);
    }

    [HttpPost]
    [Route("{id}/left")]
    [MapToApiVersion("1.0")]
    [Consumes("application/custom")]
    public IActionResult Left(string id, [FromBody]Input input)
    {
        try
        {
            diffService.StoreInputData(new InputRecord(id, Side.Left, input.input));
            return Ok();
        }

        // when called from actual web server, the format is already handled in CustomFormatter for "application/custom"
        catch (Base64JsonFormatException)
        {
            return BadRequest();
        }

        catch (DuplicateInputException)
        {
            return Conflict();
        }
    }

    [HttpPost]
    [Route("{id}/right")]
    [MapToApiVersion("1.0")]
    [Consumes("application/custom")]
    public IActionResult Right(string id, [FromBody]Input input)
    {
        try
        {
            diffService.StoreInputData(new InputRecord(id, Side.Right, input.input));
            return Ok();
        }

        // when called from actual web server, the format is already handled in CustomFormatter for "application/custom"
        catch (Base64JsonFormatException)
        {
            return BadRequest();
        }

        catch (DuplicateInputException)
        {
            return Conflict();
        }
    }
}
