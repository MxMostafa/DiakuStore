
namespace DiakuSoft.Server.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    public ProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = HttpContext.GetUserId();
        var result = await _userProfileService.GetUserProfileByIdAsync(userId);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMyProfile(UpdateUserProfileReqDto updateUserProfileReqDto)
    {
        var userId = HttpContext.GetUserId();
        var result = await _userProfileService.UpdateUserProfileAsync(userId, updateUserProfileReqDto);
        return Ok(result);
    }
}
