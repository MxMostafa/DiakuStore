

namespace DiakuSoft.Server.Domain.Dtos.Request.Auth;

public class RegisterationByEmailReqDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
