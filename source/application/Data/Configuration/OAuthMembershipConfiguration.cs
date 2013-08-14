using System.Data.Entity.ModelConfiguration;
using Intrigma.DonorSpace.Core.Domain.MemberShip;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class OAuthMembershipConfiguration : EntityTypeConfiguration<OAuthMembership>
    {
        public OAuthMembershipConfiguration()
        {
            ToTable("webpages_OAuthMembership");

            HasKey(x => new {x.Provider, x.ProviderUserId});

            Property(p => p.Provider).IsRequired().HasMaxLength(30).HasColumnType("nvarchar");

            Property(p => p.ProviderUserId).IsRequired().HasMaxLength(100).HasColumnType("nvarchar");

            Property(x => x.Id).IsRequired().HasColumnName("UserId");
        }
    }
}