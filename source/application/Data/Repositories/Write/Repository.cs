using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public class Repository<T> : RepositoryWithTypedId<T, int>,IRepository<T> where T : AggregateRoot
    {
        internal Repository(DataContext context) : base(context) { }
        
    }
}
