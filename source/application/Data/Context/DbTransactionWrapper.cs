using System.Data;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public class DbTransactionWrapper : Disposable, ITransaction
    {
        private readonly IDbTransaction _dbTransaction;
        public bool IsActive { get; private set; }

        public DbTransactionWrapper(IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            IsActive = true;
        }

        protected override void DisposeCore()
        {
            RollbackPendingTransaction();
            _dbTransaction.Dispose();
        }
        
        public void Commit()
        {
            if (IsActive)
            {
                _dbTransaction.Commit();
                IsActive = false;
            }
        }

        private void RollbackPendingTransaction()
        {
            if (IsActive)
            {
                _dbTransaction.Rollback();
                IsActive = false;
            }
        }
    }
}