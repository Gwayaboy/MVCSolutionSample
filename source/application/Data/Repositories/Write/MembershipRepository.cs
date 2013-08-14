using Intrigma.DonorSpace.Core.Domain.MemberShip;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(DataContext context) : base(context) { }
      
    }
}