using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Read;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Write
{
    public abstract class RepositoryWithTypedId<T, TId> : QueryWithTypedId<T,TId>, IRepositoryWithTypedId<T, TId>
        where T : EntityWithTypedId<TId>
    {

        protected RepositoryWithTypedId(DataContext context) : base(context) {  }


        public IList<T> SaveList(IList<T> entities)
        {
            Guard.That(entities).IsNotNull();

            foreach (var entity in entities)
            {
                AddOrUpdate(entity);
            }
            
            Context.SaveChanges();

            return entities;
        }

        public virtual T Save(T entity)
        {
            Guard.That(entity).IsNotNull();
            AddOrUpdate(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(TId id)
        {
            var entityToBeDeleted = Get(id);
            Delete(entityToBeDeleted);
        }

        private void AddOrUpdate(T entity)
        {
            if (entity.IsTransient())
            {
                Context.Add(entity);
            }
            else
            {
                Context.Update(entity);
            }
        }

        
    }
}