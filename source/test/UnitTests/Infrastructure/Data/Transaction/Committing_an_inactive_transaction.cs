using FluentAssertions;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Data.Transaction
{
    public class Committing_an_inactive_transaction : TransactionSpecification
    {
        
        public void Given_the_transaction_is_already_committed()
        {
            SUT.Commit();
        }

        public void When_committing_the_transaction()
        {
            SUT.Commit();
        }

        public void Then_the_transaction_should_not_be_committed_twice()
        {
            DbTransaction.Received(OnlyOnce).Commit();
        }

        public void AndThen_the_transaction_should_not_be_active()
        {
            SUT.IsActive.Should().BeFalse();
        }
    }
}