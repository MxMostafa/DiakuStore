using DiakuSoft.Server.Infrastructure.DbContexts;

namespace DiakuSoft.Server.Infrastructure.Repositories;
public class UserProfileRepository : IUserProfileRepository
{
    private readonly DiakuSoftDbContext _context;

    public UserProfileRepository(DiakuSoftDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetByUserIdAsync(string userId)
    {
        return await _context.UserProfiles.SingleOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<UserProfile?> UpdateAsync(UserProfile userProfile)
    {
        _context.Entry(userProfile).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userProfile;
    }
}
