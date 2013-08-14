using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Commands
{
    public class Processing_a_valid_command : CommandProcessorSpecification
    {
        private IExecutionResult _executionResult;
        
        public void When_invoking_command()
        {
            _executionResult = SUT.Execute(Command);
        }

        public void Then_the_UnitOfWork_should_begin_a_transaction()
        {
            UnitOfWork.Received(OnlyOnce).BeginTransaction();
        }

       public void AndThen_its_command_handler_should_be_invoked()
        {
            CommandHandler.Received(OnlyOnce).Handle();
        }

        public void AndThen_The_Transaction_should_be_committed()
        {
            Transaction.Received(OnlyOnce).Commit();
        }

        public void AndThen_the_Execution_result_should_be_successfull()
        {
            _executionResult.Should().BeSuccessfull();
        }
    }
}