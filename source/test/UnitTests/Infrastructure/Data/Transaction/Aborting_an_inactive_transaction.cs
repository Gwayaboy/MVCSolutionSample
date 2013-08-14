using FluentAssertions;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Data.Transaction
{
    public class Aborting_an_inactive_transaction : TransactionSpecification
    {
        public void Given_the_transaction_has_been_committed()
        {
            SUT.Commit();
        }
        
        public void When_Disposing_the_transaction()
        {
            SUT.Dispose();
        }

        public void Then_the_pending_transaction_should_not_be_committed_twice()
        {
            DbTransaction.Received(OnlyOnce).Commit();
        }

        public void Then_the_pending_transaction_should_not_be_rollbacked()
        {
            DbTransaction.DidNotReceive().Rollback();
        }

        public void AndThen_the_database_connection_should_be_disposed()
        {
            DbTransaction.Received(OnlyOnce).Dispose();
        }

        public void AndThen_the_transaction_should_not_be_active()
        {
            SUT.IsActive.Should().BeFalse();
        }
    }
}