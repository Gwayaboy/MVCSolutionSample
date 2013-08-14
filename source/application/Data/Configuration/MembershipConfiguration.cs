using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Intrigma.DonorSpace.Core.Domain.MemberShip;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class MembershipConfiguration : EntityTypeConfiguration<Membership>
    {
        public MembershipConfiguration()
        {
            ToTable("webpages_Membership");

            Property(p => p.Id)
                .HasColumnName("UserId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.ConfirmationToken).HasMaxLength(128).HasColumnType("nvarchar");

            Property(p => p.PasswordFailuresSinceLastSuccess).IsRequired();

            Property(p => p.Password).IsRequired().HasMaxLength(128).HasColumnType("nvarchar");

            Property(p => p.PasswordSalt).IsRequired().HasMaxLength(128).HasColumnType("nvarchar");

            Property(p => p.PasswordVerificationToken).HasMaxLength(128).HasColumnType("nvarchar");

        }
    }
}