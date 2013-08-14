using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Read;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Repositories.Write;

namespace Intrigma.DonorSpace.Acceptance.Helper
{
    public class Database : IDatabase
    {
        private readonly DataContext _context;
        private readonly IDatabaseInitialiser _databaseInitialiser;

        public Database(DataContext context, IDatabaseInitialiser databaseInitialiser  )
        {
            _context = context;
            _databaseInitialiser = databaseInitialiser;
        }


        public void ResetData()
        {
            DeleteAllData();
        }

        public T Save<T>(T entity) where T : AggregateRoot
        {
            return AddOne(entity);
        }

        public IList<T> SaveList<T>(IList<T> entities) where T : AggregateRoot
        {
            return Repository<T>().SaveList(entities);
        }

        public IRepository<T> Repository<T>() where T : AggregateRoot
        {
            return new Repository<T>(_context);
        }

        public IQuery<T> Query<T>() where T : Entity
        {
            return new Query<T>(_context);
        }

      

        private T AddOne<T>(T entity) where T : AggregateRoot
        {
            return Repository<T>().Save(entity);

        }

        private void DeleteAllData()
        {
            _databaseInitialiser.Initialise();

            var sets =
              _context
                  .GetType()
                  .GetProperties()
                  .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>))
                  .Select(p => (dynamic)p.GetValue(_context, null))
                  .ToList();


            foreach (var set in sets)
            {
                foreach (var entity in set)
                {
                    //if (entity is AggregateRoot)
                    {
                        set.Remove(entity);
                    }
                }
            }
        }
    }
}