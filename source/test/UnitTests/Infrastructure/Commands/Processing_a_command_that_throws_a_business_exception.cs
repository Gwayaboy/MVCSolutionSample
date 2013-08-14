using System;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Commands
{
    public class Processing_a_command_that_throws_a_business_exception : CommandProcessorSpecification
    {
        protected static Exception Exception;
        private IExecutionResult _executionResult;
        private const string BrokenBusinessRuleMessage = "Some business logic is broken";

        public void Given_the_command_handler_throws_a_Business_Exception()
        {
            CommandHandler
                .When(x => x.Handle())
                .Do(x => { throw new BusinessRuleException(BrokenBusinessRuleMessage); });
        }

        public void When_invoking_command()
        {
            _executionResult = SUT.Execute(Command);
        }

        public void Then_the_Execution_result_should_be_unsuccessfull_with_a_BrokenBusinessRule()
        {
            _executionResult.Should().BeUnsuccessfull().And.HaveErrorMessageOfType(MessageCategory.BrokenBusinessRule);
        }

        public void AndThen_The_Execution_Result_should_have_the_corresponding_error_message()
        {
            _executionResult.Should().HaveErrorMessage(BrokenBusinessRuleMessage);
        }

        public void AndThen_The_UnitOfWork_should_begin_a_transaction()
        {
            UnitOfWork.Received(OnlyOnce).BeginTransaction();
        }

        public void AndThen_The_transaction_should_not_be_committed()
        {
            Transaction.DidNotReceive().Commit();
        }
    }
}