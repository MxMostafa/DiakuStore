namespace DiakuSoft.Server.Domain.Dtos.Request.Auth;

public class LoginReqDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
