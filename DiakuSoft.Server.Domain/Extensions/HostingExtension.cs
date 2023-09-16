

namespace DiakuSoft.Server.Domain.Extensions;

public static class HostingExtension
{
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
