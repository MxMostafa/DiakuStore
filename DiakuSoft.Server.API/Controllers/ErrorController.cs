
namespace DiakuSoft.Server.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    public IActionResult Error()
    {
        var exceprion = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        _logger.LogError(exceprion ?? new Exception("Exception is null"), exceprion?.Message);

        return Problem(exceprion?.Message,statusCode: (int)HttpStatusCode.InternalServerError);
    }
}
