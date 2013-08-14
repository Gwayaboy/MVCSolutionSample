using System;
using Panzea.DonorSpace.Core.Interfaces.Data.Write;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;
using Panzea.DonorSpace.Core.Domain.Base;
using Panzea.DonorSpace.Infrastructure.Helper;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public class Repository<T> : RepositoryWithTypedId<T, Guid>,IRepository<T> where T : AggregateRoot
    {
        internal Repository(DataContext context) : base(context) { }

        public override Guid GenerateId()
        {
           return GuidComb.Generate();
        }

        public override T Get(Guid id)
        {
            return FindOne(entity => entity.Id == id);
        }
        
    }
}
