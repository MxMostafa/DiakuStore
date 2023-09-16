namespace DiakuSoft.Server.Domain.Dtos.Request.Auth;

public class UpdateUserProfileReqDto
{
    public string? FirsName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
}
