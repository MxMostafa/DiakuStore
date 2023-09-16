



namespace DiakuSoft.Server.Infrastructure.Extensions;

public static class HostingExtension
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        return services;
    }
}
