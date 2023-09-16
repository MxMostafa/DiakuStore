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
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTKeySetting:ValidAudience"],
                    ValidIssuer = configuration["JWTKeySetting:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKeySetting:Secret"]!))
                };
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
