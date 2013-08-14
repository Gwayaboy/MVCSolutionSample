using System.Data;
using System.Data.Entity.Infrastructure;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private ITransaction _transaction;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ITransaction BeginTransaction()
        {
            if (_transaction == null)
            {
                IObjectContextAdapter objectContextAdapter = _context;
                var connection = objectContextAdapter.ObjectContext.Connection;
                if ((connection.State &
                     ConnectionState.Open) != ConnectionState.Open)
                {
                    connection.Open();
                }
                
                _transaction = new DbTransactionWrapper(connection.BeginTransaction());
            }

            return _transaction;
        }

    }
}