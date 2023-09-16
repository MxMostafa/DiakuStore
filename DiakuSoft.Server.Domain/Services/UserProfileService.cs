

using DiakuSoft.Server.Domain.Interfaces.Repository;

namespace DiakuSoft.Server.Domain.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileService(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<ApiResponse<UserProfileResDto>> GetUserProfileByIdAsync(string userId)
    {
        var userProfile = await _userProfileRepository.GetByUserIdAsync(userId);
        if (userProfile == null) return new ApiResponse<UserProfileResDto>(System.Net.HttpStatusCode.NotFound, ResponseMessages.UserNotFound);

        var result = new UserProfileResDto()
        {
            Birthday = userProfile.Birthday,
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName
        };

        return new ApiResponse<UserProfileResDto>(result);
    }

    public async Task<ApiResponse<UserProfileResDto>> UpdateUserProfileAsync(string userId, UpdateUserProfileReqDto updateUserProfileReqDto)
    {
        var userProfile = await _userProfileRepository.GetByUserIdAsync(userId);
        if (userProfile == null) return new ApiResponse<UserProfileResDto>(System.Net.HttpStatusCode.NotFound, ResponseMessages.UserNotFound);

        userProfile.Birthday = updateUserProfileReqDto.Birthday;
        userProfile.FirstName = updateUserProfileReqDto.FirsName;
        userProfile.LastName = updateUserProfileReqDto.LastName;

        await _userProfileRepository.UpdateAsync(userProfile);

        var result = new UserProfileResDto()
        {
            Birthday = userProfile.Birthday,
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName
        };
        return new ApiResponse<UserProfileResDto>(result);
    }
}
