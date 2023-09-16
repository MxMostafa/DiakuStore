

namespace DiakuSoft.Server.Domain.Interfaces.Service;
public interface IAuthService
{
    Task<ApiResponse< LoginResDto>> LoginAsync(LoginReqDto loginReqDto);
    Task<ApiResponse<bool>> RegisterByEmailAsync(RegisterationByEmailReqDto model);
}
