using System;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Commands.User;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Core.Commands.User
{
    public class Failing_to_retrieve_specified_Charity : SpecificationFor<AuthenticateUserCommandHandler>
    {
        private Action _retrieveCharityAction;
        private const string UnknownSpecifiedCharityMessage = "The specified charity is unknown";

        public void Given_the_Charity_does_not_exist()
        {
            Charity nullCharity = null;
            SubstituteFor<ICharityRepository>().Get(SUT.Command.CharityId).Returns(nullCharity);
        }

        public void When_authenticating_the_Charity_User()
        {
            _retrieveCharityAction = () => SUT.Handle();
        }

        public void Then_a_Business_Rule_Exception_should_be_thrown_with_failure_message()
        {
            _retrieveCharityAction
                .ShouldThrow<BusinessRuleException>()
                .WithMessage(UnknownSpecifiedCharityMessage);
        }
    }
}