using FluentAssertions;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Data.Transaction
{
    public class Aborting_an_active_transaction : TransactionSpecification
    {
        public void When_Disposing_the_transaction()
        {
            SUT.Dispose();
        }

        public void Then_the_pending_transaction_should_not_be_committed()
        {
            DbTransaction.DidNotReceive().Commit();
        }

        public void Then_the_pending_transaction_should_be_rollbacked()
        {
            DbTransaction.Received(OnlyOnce).Rollback();
        }
        
        public void AndThen_the_database_connection_disposed()
        {
            DbTransaction.Received(OnlyOnce).Dispose();
        }

        public void AndThen_the_transaction_should_not_be_active()
        {
            SUT.IsActive.Should().BeFalse();
        }


    }
}