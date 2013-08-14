using System;
using FluentAssertions;
using FluentAssertions.Specialized;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.ObjectMapping
{
    public class Failing_to_Map_a_Form_with_no_mapping_configuration : ModelMapperSpecification
    {
        private Action _failingToMapFormAction;
        private ExceptionAssertions<ArgumentException> _argumentExceptionAssertion;

        public void Given_the_Form_has_no_mapping_configuration()
        {
            Mapper.GetMappingConfigurationMatching(FormType).Returns(NullMappingConfiguration);
        }

        public void When_mapping_a_form_to_its_corresponding_command()
        {
            _failingToMapFormAction = () => SUT.MapFormToCommand(Form);
        }

        public void Then_an_ArgumentException_should_be_thrown()
        {
            _argumentExceptionAssertion = _failingToMapFormAction.ShouldThrow<ArgumentException>();

        }

        public void AndThen_the_exception_error_message_should_be_There_is_no_command_associated_with_Form_FakeForm()
        {
            _argumentExceptionAssertion.WithMessage("There is no Command associated with Form FakeForm");
        }

    }
}