using Microsoft.Extensions.Options;

namespace DiakuSoft.Server.API.Extensions;
public static class HostingExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(setup =>
        {


            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Diaku Store Server" });

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    jwtSecurityScheme, Array.Empty<string>()
                }
            });

        });

        services.AddDbContext<DiakuSoftDbContext>(
         option => option.UseSqlServer("name=ConnectionStrings:DefaultConnection"));



        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DiakuSoftDbContext>().AddDefaultTokenProviders();

        var jWTKeySetting = configuration.GetSection("JWTKeySetting");
        services.Configure<JWTKeySetting>(jWTKeySetting);

        services.ConfigureDomain();
        services.ConfigureInfrastructure();

        var secret = configuration["JWTKeySetting:Secret"]!;
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromHours(1),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            };
        });

        return services;
    }

    public static string GetUserId(this HttpContext context)
    {
        var userId = "";

        if (context.User.Identities.Count() > 0)
        {
            var identity = context.User.Identities.First();
            var claims = identity.Claims.Where(x => x.Type.Contains("nameidentifier"));
            if (claims.Count() > 0)
                userId = claims.First().Value.ToString();
        }

        return userId;
    }
}
