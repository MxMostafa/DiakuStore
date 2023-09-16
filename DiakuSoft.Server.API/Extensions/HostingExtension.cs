using DiakuSoft.Server.Domain.Entities.Auth;
using DiakuSoft.Server.Domain.Extensions;
using DiakuSoft.Server.Domain.Models;
using DiakuSoft.Server.Infrastructure.DbContexts;
using DiakuSoft.Server.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DiakuSoft.Server.API.Extensions;
public static class HostingExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DiakuSoftDbContext>(
         option => option.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {

            });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DiakuSoftDbContext>().AddDefaultTokenProviders();

        var jWTKeySetting = configuration.GetSection("JWTKeySetting");
        services.Configure<JWTKeySetting>(jWTKeySetting);

        services.ConfigureDomain();
        services.ConfigureInfrastructure();

        return services;
    }
}
