using Intrigma.DonorSpace.Core.Domain;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class CharityConfiguration : EntityIdIncrementConfiguration<Charity>
    {
        public CharityConfiguration()
        {
            Ignore(x => x.Donors);
            HasMany(x => x.DonorCollection);

            Ignore(x => x.Administrators);
            HasMany(x => x.AdministratorCollection);
        }
    }

   
}
