using System.Data.Entity;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation
{
    public class NullModelStorageInitialiser : IModelStorageStrategyInitialiser
    {
        private class EntityFrameworkNullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> 
            where TContext : DbContext
        {
            public void InitializeDatabase(TContext context)
            {
            }
        }

        public void Apply()
        {
            Database.SetInitializer(new EntityFrameworkNullDatabaseInitializer<DataContext>());
        }
    }
}