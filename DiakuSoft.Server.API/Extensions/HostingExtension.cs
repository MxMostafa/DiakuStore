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

    public static string GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.Identity?.Name ?? string.Empty;
    }
}
