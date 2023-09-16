
namespace DiakuSoft.Server.Domain.Entities.Auth;
public class ApplicationUser: IdentityUser
{
	public ApplicationUser()
	{
		UserProfile = new UserProfile();
	}
    public UserProfile UserProfile { get; set; } = null!;
}
