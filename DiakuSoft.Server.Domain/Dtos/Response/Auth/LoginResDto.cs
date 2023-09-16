
namespace DiakuSoft.Server.Domain.Dtos.Response.Auth;

public class LoginResDto
{
    public string AccessToken { get; set; } = null!;
    public UserProfileResDto? Profile { get; set; }
}
