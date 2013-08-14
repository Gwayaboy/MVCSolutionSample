using System;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Commands
{
    public class Processing_a_command_that_does_not_have_a_handler : CommandProcessorSpecification
    {
        protected static Exception Exception;
        private IExecutionResult _executionResult;

        protected override Func<Type, ICommandHandler> CommandHandlerFactory { get { return t => null; } }

        public void Given_the_command_does_not_have_a_handler(){}

        public void When_invoking_command()
        {
            _executionResult = SUT.Execute(Command);
        }

        public void Then_the_Execution_result_should_be_unsuccessfull_with_a_Fatal_Exception()
        {
            _executionResult.Should().BeUnsuccessfull().And.HaveErrorMessageOfType(MessageCategory.FatalException);   
        }

        public void AndThen_the_error_message_should_be_Command_handler_not_found_for_command_type()
        {
            _executionResult.Should().HaveErrorMessage("Command handler not found for command type: FakeCommand");
        }
    }
}