using System.Linq;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public class CharityRepository : Repository<Charity>,ICharityRepository
    {
        public CharityRepository(DataContext context) : base(context)
        {
        }

        public override Charity Get(int id)
        {
            return
                QueryAllWith(c => c.Id == id, c => c.AdministratorCollection).SingleOrDefault();
        }
    }
}