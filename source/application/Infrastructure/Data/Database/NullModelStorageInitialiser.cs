using System.Data.Entity;
using Panzea.DonorSpace.Core.Interfaces.Data;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Database
{
    public class NullModelStorageInitialiser : IModelStorageStrategyInitialiser
    {
        internal class EntityFrameworkNullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
        {
            public void InitializeDatabase(TContext context)
            {
            }
        }

        public void Initialise()
        {
            System.Data.Entity.Database.SetInitializer(new EntityFrameworkNullDatabaseInitializer<DataContext>());
        }
    }
}