
namespace DiakuSoft.Server.Domain.Interfaces.Service;

public interface IUserProfileService
{
    Task<ApiResponse<UserProfileResDto>> GetUserProfileByIdAsync(string userId);
    Task<ApiResponse<UserProfileResDto>> UpdateUserProfileAsync(string userId, UpdateUserProfileReqDto updateUserProfileReqDto);
}
