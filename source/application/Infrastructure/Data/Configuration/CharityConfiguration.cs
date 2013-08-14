using System.Data.Entity.ModelConfiguration;
using Panzea.DonorSpace.Core.Domain;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class CharityConfiguration : EntityTypeConfiguration<Charity>
    {
        public CharityConfiguration()
        {
            Ignore(x => x.Donors);
            HasMany(x => x.InternalDonors);

            Ignore(x => x.Administrators);
            HasMany(x => x.InternalAdministrators);
        }
    }
}
