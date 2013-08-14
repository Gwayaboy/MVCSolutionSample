using System.Data.Entity;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation
{
    public class DatabaseInitialiser : IDatabaseInitialiser
    {
        private readonly IModelStorageStrategyInitialiser _storageStrategyInitialiser;
        private readonly Database _database;

     
        public DatabaseInitialiser(IModelStorageStrategyInitialiser storageStrategyInitialiser, Database database)
        {
            _storageStrategyInitialiser = storageStrategyInitialiser;
            _database = database;
        }

        public void Initialise(bool forceReinitialisation)
        {
            _storageStrategyInitialiser.Apply();
            _database.Connection.Close();
            _database.Initialize(forceReinitialisation);
            _database.Connection.Open();
        }
    }
}