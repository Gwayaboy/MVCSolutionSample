using System;
using Panzea.DonorSpace.Core.Interfaces.Data.Read;
using Panzea.DonorSpace.Core.Domain.Base;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Repositories.Read
{
    public class Query<T> : QueryWithTypedId<T,Guid>, IQuery<T>
        where T : Entity
    {
        public Query(DataContext context) : base(context) { }

        public override T Get(Guid id)
        {
            return FindOne(entity => entity.Id == id);
        }
    }
}