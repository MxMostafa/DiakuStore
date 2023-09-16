



namespace DiakuSoft.Server.Infrastructure.DbContexts.EntityTypeConfigurations;

public class UserProfileEntityTypeConfigurations : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasOne(p => p.ApplicationUser)
             .WithOne(p => p.UserProfile)
             .HasForeignKey<UserProfile>(p => p.UserId);
    }
}
