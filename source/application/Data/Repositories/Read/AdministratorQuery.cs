using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Read
{
    public class AdministratorQuery : Query<Administrator>,  IAdministratorQuery
    {
        public AdministratorQuery(DataContext context) : base(context) { }

        public Administrator FindByUserName(string userName)
        {
            return FindOne(x => x.UserName == userName);
        }
    }
}