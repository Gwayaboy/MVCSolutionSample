using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;

namespace Intrigma.DonorSpace.Acceptance.Helper
{
    public interface IDatabase
    {
        void ResetData();
        T Save<T>(T entity) where T : AggregateRoot;
        IList<T> SaveList<T>(IList<T> entities) where T : AggregateRoot;
        IRepository<T> Repository<T>() where T : AggregateRoot;
        IQuery<T> Query<T>() where T : Entity;
    }
}