

namespace DiakuSoft.Server.Domain.Interfaces.Repository;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByUserIdAsync(string userId);
    Task<UserProfile?> UpdateAsync(UserProfile userProfile);
}
