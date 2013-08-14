using System;
using FluentAssertions;
using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Commands
{
    public class Processing_a_null_command : CommandProcessorSpecification
    {
        private readonly ICommand _nullCommand = null;
        private readonly Action _processingNullCommand;

        public Processing_a_null_command()
        {
            _processingNullCommand = () => SUT.Execute(_nullCommand);
        }

        public void Then_an_ArgumentNullException_should_be_thrown()
        {
            _processingNullCommand
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("Value cannot be null.\r\nParameter name: command");
        }
    }
}