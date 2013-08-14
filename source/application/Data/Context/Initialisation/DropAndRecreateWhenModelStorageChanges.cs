using System.Data.Entity;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation
{
    
    public class DropAndRecreateWhenModelStorageChanges : DropCreateDatabaseIfModelChanges<DataContext>,
                                                                     IModelStorageStrategyInitialiser
    {
        private readonly IPopulateDataHandler<DataContext> _populateDataHandler;

        public DropAndRecreateWhenModelStorageChanges(IPopulateDataHandler<DataContext> populateDataHandler)
        {
            _populateDataHandler = populateDataHandler;
        }

        public void Apply()
        {
            Database.SetInitializer(this);
        }

        protected override void Seed(DataContext context)
        {
           _populateDataHandler.Populate(context);
            base.Seed(context);
        }


    }
}