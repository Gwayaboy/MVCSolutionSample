using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;

namespace Intrigma.DonorSpace.Core.Interfaces.Data.Write
{
    /// <summary>
    ///     Provides a standard interface for Repositories which are data-access mechanism agnostic.
    /// 
    ///     Since nearly all of the domain objects you create will have a type of Guid Id, this 
    ///     base IRepository assumes that.  
    ///     if the entity Id type is changed here then it needs to be changed at <see cref="Domain.Base.Entity"/> and
    ///     at <see cref="IQuery{T}"/>
    /// </summary>
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : AggregateRoot { }

    public interface IRepositoryWithTypedId<T, TId> : IQueryWithTypedId<T,TId> 
        where T : EntityWithTypedId<TId>
    {
        
        /// <summary>
        /// For entities with automatically generated Ids, such as identity or Hi/Lo, Save may 
        /// be called when saving or updating an entity.  If you require separate Save and Update
        /// methods.
        /// Updating also allows you to commit changes to a detached object.  More info may be found at:
        /// </summary>
        T Save(T entity);

        IList<T> SaveList(IList<T> entities);

      
        void Delete(T entity);


        void Delete(TId id);
    }
}