
namespace DiakuSoft.Server.API.Extensions;
public static  class ValidationExtensions
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LoginReqDto>, LoginReqDtoValidator>();
        services.AddScoped<IValidator<RegisterationByEmailReqDto>, RegistrationWithByValidator>();
        return services;
    }
}
