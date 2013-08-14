using System.Data.Entity;
using Panzea.DonorSpace.Core.Interfaces.Data;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Database
{
    public class AlwaysDropAndRecreateDatabaseStorage : DropCreateDatabaseAlways<DataContext>, IModelStorageStrategyInitialiser
    {
      
        public void Initialise()
        {
            System.Data.Entity.Database.SetInitializer(this);
        }
    }
}