using Microsoft.AspNetCore.Mvc;
using SkiNet.Svc.Errors;

namespace SkiNet.Svc.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("errors/{code}")]
public class ErrorController : BaseApiController
{
  public ActionResult Error(int code)
  {
    return new ObjectResult(new ApiResponse(code));
  }
}