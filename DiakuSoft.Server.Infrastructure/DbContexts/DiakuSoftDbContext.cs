



using Microsoft.AspNetCore.Identity;

namespace DiakuSoft.Server.Infrastructure.DbContexts;
public class DiakuSoftDbContext : IdentityDbContext<ApplicationUser>
{
    public DiakuSoftDbContext(DbContextOptions<DiakuSoftDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      

    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; } 
}
