namespace DiakuSoft.Server.Infrastructure.DbContexts.EntityTypeConfigurations;
public class UserApplicationEntityTypeConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users","dbo").Property(p => p.Id).HasColumnName("User_Id"); ;
    }
}
