using DiakuSoft.Server.Domain.Interfaces.Repository;
using DiakuSoft.Server.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace DiakuSoft.Server.Domain.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWTKeySetting _jWTKeySetting;
    private readonly IUserProfileRepository _userProfileRepository;
    public AuthService(IOptions<JWTKeySetting> jwtOptions, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserProfileRepository userProfileRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jWTKeySetting = jwtOptions.Value;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<ApiResponse<bool>> RegisterByEmailAsync(RegisterationByEmailReqDto model)
    {

        var userExists = await _userManager.FindByNameAsync(model.Email!);
        if (userExists != null)
            return new ApiResponse<bool>(HttpStatusCode.NotAcceptable, ResponseMessages.UserNameAlreadyExist);

        var user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            UserProfile = new UserProfile() { FirstName = model.FirstName, LastName = model.LastName }
        };

        var createUserResult = await _userManager.CreateAsync(user, model.Password!);

        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(",", createUserResult.Errors.Select(P => P.Description).ToList());
            return new ApiResponse<bool>(HttpStatusCode.BadRequest, $"{ResponseMessages.ServerError} : {errors}");
        }

        var defaultRole = "user";
        if (!await _roleManager.RoleExistsAsync(defaultRole))
            await _roleManager.CreateAsync(new IdentityRole(defaultRole));

        if (await _roleManager.RoleExistsAsync(defaultRole))
            await _userManager.AddToRoleAsync(user, defaultRole);

        return new ApiResponse<bool>(true);
    }


    public async Task<ApiResponse<LoginResDto>> LoginAsync(LoginReqDto loginReqDto)
    {
        var user = await _userManager.FindByNameAsync(loginReqDto.UserName);
        if (user == null)
            return new ApiResponse<LoginResDto>(System.Net.HttpStatusCode.NotFound, ResponseMessages.UserNotFound);

        if (!await _userManager.CheckPasswordAsync(user, loginReqDto.Password))
        {
            return new ApiResponse<LoginResDto>(System.Net.HttpStatusCode.NotFound, ResponseMessages.Invalid_Password);
        }

        var userProfile = await _userProfileRepository.GetByUserIdAsync(user.Id);

        var authClaims = new List<Claim>()
        {
            new Claim("id",user.Id!),
            new Claim("firstname",userProfile?.FirstName??string.Empty),
            new Claim("lastname",userProfile?.LastName??string.Empty)
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = await GenerateToken(authClaims);

        if (token != null)
        {
            var result = new LoginResDto()
            {
                AccessToken = token,
                Profile = new ProfileResDto()
                {
                    FirstName = userProfile?.FirstName,
                    LastName = userProfile?.LastName,
                    Birthday = userProfile?.Birthday
                }
            };
            return new ApiResponse<LoginResDto>(result);
        }

        return new ApiResponse<LoginResDto>(System.Net.HttpStatusCode.InternalServerError, ResponseMessages.ServerError);
    }


    private async Task<string> GenerateToken(IEnumerable<Claim> claims)
    {
        return await Task.Run(() =>
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTKeySetting.Secret));
            var tokenExpiryTimeInHour = Convert.ToInt64(_jWTKeySetting.TokenExpiryTimeInHour);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jWTKeySetting.ValidIssuer,
                Audience = _jWTKeySetting.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        });
    }
}
