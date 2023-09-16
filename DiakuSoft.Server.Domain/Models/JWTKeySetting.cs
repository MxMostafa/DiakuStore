

namespace DiakuSoft.Server.Domain.Models;

public class JWTKeySetting
{
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public int TokenExpiryTimeInHour { get; set; } 
    public string Secret { get; set; } = null!;
}
