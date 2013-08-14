using Intrigma.DonorSpace.Core.Domain;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class AdministratorConfiguration : EntityIdIncrementConfiguration<Administrator>
    {
        public AdministratorConfiguration()
        {
            Property(x => x.UserName).IsRequired();

            HasMany(x => x.RoleCollection)
                .WithMany(x => x.AdministratorCollection)
                .Map(x =>
                    {
                        x.MapLeftKey("UserId");
                        x.MapRightKey("RoleId");
                        x.ToTable("webpages_UsersInRoles");
                    });
        }
    }

    

}