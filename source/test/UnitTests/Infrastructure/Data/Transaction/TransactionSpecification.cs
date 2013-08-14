using System.Data;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Data.Transaction
{
    public abstract class TransactionSpecification : SpecificationFor<DbTransactionWrapper>
    {
        protected IDbTransaction DbTransaction { get; private set; }

        protected TransactionSpecification()
        {
            DbTransaction = SubstituteFor<IDbTransaction>();

        }
    }
}