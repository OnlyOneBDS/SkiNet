using Microsoft.AspNetCore.Authorization;
using SkiNet.Svc.Errors;

namespace SkiNet.Svc.Controllers;

public class BuggyController : BaseApiController
{
  private readonly StoreContext _context;

  public BuggyController(StoreContext context)
  {
    _context = context;
  }

  [Authorize]
  [HttpGet("testauth")]
  public ActionResult<string> GetSecretText()
  {
    return "secret stuff";
  }

  [HttpGet("notfound")]
  public ActionResult GetNotFoundRequest()
  {
    var thing = _context.Products.Find(42);

    if (thing == null)
    {
      return NotFound(new ApiResponse(400));
    }

    return Ok();
  }

  [HttpGet("servererror")]
  public ActionResult GetServerError()
  {
    var thing = _context.Products.Find(42);
    var thingToReturn = thing.ToString();

    return Ok();
  }

  [HttpGet("badrequest")]
  public ActionResult GetBadRequest()
  {
    return BadRequest(new ApiResponse(400));
  }

  [HttpGet("badrequest/{id}")]
  public ActionResult GetNotFoundRequest(int id)
  {
    return Ok();
  }
}