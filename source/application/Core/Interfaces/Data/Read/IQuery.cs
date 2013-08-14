using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Interfaces.Data.Read
{
    public interface IQuery<T> : IQueryWithTypedId<T,int> where T : EntityWithTypedId<int> { }


    public interface IQueryWithTypedId<T, TId> where T : EntityWithTypedId<TId>
    {

        /// <summary>
        /// Returns null if a row is not found matching the provided Id.
        /// </summary>
        T Get(TId id);

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <param name="predicate">when provided will filter entity statisfying the predicate</param>
        IList<T> GetAll(Expression<Func<T, bool>> predicate = null);

      
        /// <summary>
        /// Get one entity matching a condition
        /// </summary>
        /// <param name="predicate">will ensure only one or none statisfy the predicate</param>
        T FindOne(Expression<Func<T, bool>> predicate);

        Boolean Exists(Expression<Func<T, bool>> predicate);

    }
}