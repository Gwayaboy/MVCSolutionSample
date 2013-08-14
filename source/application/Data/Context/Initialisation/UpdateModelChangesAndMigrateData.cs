using System;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation
{
    /// <summary>
    /// when needed
    /// http://blogs.msdn.com/b/adonet/archive/2012/02/09/ef-4-3-code-based-migrations-walkthrough.aspx
    /// </summary>
    public class UpdateModelChangesAndMigrateData : IModelStorageStrategyInitialiser
    {
        public void Apply()
        {
            throw new NotImplementedException();
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, DbMigrationsConfiguration<DataContext>>());
            
        }
    }
}
