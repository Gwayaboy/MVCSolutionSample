using System.Data.Entity;
using Panzea.DonorSpace.Core.Interfaces.Data;
using Panzea.DonorSpace.Infrastructure.Data.EF.Context;

namespace Panzea.DonorSpace.Infrastructure.Data.EF.Database
{
    public class DropAndRecreateWhenModelStorageChanges : DropCreateDatabaseIfModelChanges<DataContext>,
                                                                     IModelStorageStrategyInitialiser
    {

        public void Initialise()
        {
            System.Data.Entity.Database.SetInitializer(this);
        }
    }
}