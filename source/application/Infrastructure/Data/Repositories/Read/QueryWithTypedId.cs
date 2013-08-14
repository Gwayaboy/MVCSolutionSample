using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Panzea.DonorSpace.Core.Domain.Base;
using Panzea.DonorSpace.Core.Interfaces.Data.Read;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;
using Seterlund.CodeGuard;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Repositories.Read
{
    public abstract class QueryWithTypedId<T,TId> : IQueryWithTypedId<T,TId>
        where T : EntityWithTypedId<TId>
    {
        public IDbSet<T> EntityCollection { get; set; }

        protected QueryWithTypedId(DataContext context)
        {
            Guard.That(context).IsNotNull();
            Context = context;
        }

        public virtual DataContext Context { get; private set; }

        public abstract T Get(TId id);

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return QueryAllWith(predicate);
        }

        public virtual T FindOne(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().SingleOrDefault(predicate);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return FindOne(predicate) != null;
        }

        protected IList<T> QueryAllWith(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] entitiesToBeincluded)
        {
            var query = Context.CreateQueryWith(entitiesToBeincluded);

            return (predicate == null) ? query.ToList() : query.Where(predicate).ToList();
        }
    }
}