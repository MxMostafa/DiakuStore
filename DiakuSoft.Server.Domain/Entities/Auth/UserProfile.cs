

namespace DiakuSoft.Server.Domain.Entities.Auth;

public class UserProfile : BaseEntity<Guid>
{
    public string? UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
}
