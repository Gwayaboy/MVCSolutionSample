using System.Linq;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Read
{
    public class CharityQuery : Query<Charity>, ICharityQuery
    {
        public CharityQuery(DataContext context) : base(context)
        {
        }

        public override Charity Get(int id)
        {
            return
                QueryAllWith(c => c.Id == id, c => c.AdministratorCollection).SingleOrDefault();
        }
    }
}