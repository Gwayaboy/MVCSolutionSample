using System;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Commands.User;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Core.Commands.User
{
    
    public class Failing_to_authenticate_a_regular_user : AuthenticateUserCommandHandlerSpecification
    {
        private Action _authenticateUserAction;
        private readonly string _userFailedToAuthenticateMessage = string.Format("User {0} has failed to authenticate", UserName);

        public void Given_the_Charity_exists()
        {
            SubstituteFor<ICharityRepository>().Get(SUT.Command.CharityId).Returns(Charity);
        }

        public void AndGiven_the_Charity_has_no_Administrator_matching_the_username_being_authenticated()
        {
            Charity.IsAdministrator(UserName).Returns(false);
        }

        public void When_authenticating_the_charity_user()
        {
            _authenticateUserAction = () => SUT.Handle();
        }

        public void Then_a_Business_Rule_Exception_should_be_thrown_with_failure_message()
        {
            _authenticateUserAction
                .ShouldThrow<BusinessRuleException>()
                .WithMessage(_userFailedToAuthenticateMessage)
                .WithPropertiesOf<AuthenticateUserCommand>();
        }
    }
}