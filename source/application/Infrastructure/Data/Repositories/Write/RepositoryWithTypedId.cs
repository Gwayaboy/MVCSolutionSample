using System.Collections.Generic;
using System.Linq;
using Panzea.DonorSpace.Core.Domain.Base;
using Panzea.DonorSpace.Core.Interfaces.Data.Write;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;
using Panzea.DonorSpace.Infrastructure.Data.EF.Repositories.Read;
using Panzea.DonorSpace.Infrastructure.Extensions;
using Seterlund.CodeGuard;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public abstract class RepositoryWithTypedId<T, TId> : QueryWithTypedId<T,TId>, IRepositoryWithTypedId<T, TId>
        where T : EntityWithTypedId<TId>
    {

        protected RepositoryWithTypedId(DataContext context) : base(context) {  }

        public abstract TId GenerateId();

        public IList<T> SaveList(IList<T> entities)
        {
            Guard.That(entities).IsNotNull();
            
            entities.ToList().ForEach(AddToContextIfTransient);

            Context.SaveChanges();

            return entities;
        }

        public virtual T SaveOrUpdate(T entity)
        {
            Guard.That(entity).IsNotNull();

            AddToContextIfTransient(entity);
            Context.SaveChanges();
            return entity;
        }


        public virtual void Delete(T entity) 
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

       

        private void AddToContextIfTransient(T entity)
        {
            if (entity.IsTransient())
            {
                entity.SetId(GenerateId());
                Context.Set<T>().Add(entity);    
            }
        }

    }
}