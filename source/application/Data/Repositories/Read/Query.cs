using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Read
{
    public class Query<T> : QueryWithTypedId<T, int>, IQuery<T>
        where T : Entity
    {
        public Query(DataContext context) : base(context)
        {
        }
    }
}