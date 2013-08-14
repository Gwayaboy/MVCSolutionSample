using System;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Fakes;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Web.FormProcessor
{
    public class Processing_a_null_form : FormProcessorSpecification
    {
        private readonly FakeForm _nullForm = null;
        private readonly Action _processingNullCommand;

        public Processing_a_null_form()
        {
            _processingNullCommand = () => SUT.Execute(_nullForm);
        }

        public void Then_an_ArgumentNullException_should_be_thrown()
        {
            _processingNullCommand
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("Value cannot be null.\r\nParameter name: form");
        }


    }
}