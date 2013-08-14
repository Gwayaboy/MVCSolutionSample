using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Core.Commands.User
{
    public class Successfully_authenticating_a_regular_user_on_premise : AuthenticateUserCommandHandlerSpecification
    {

        public void Given_the_Charity_exists()
        {
            AuthenticationOnPremiseService
                .Login(SUT.Command.UserName, SUT.Command.Password, SUT.Command.RememberMe)
                .Returns(true);

            SubstituteFor<ICharityRepository>()
                .Get(SUT.Command.CharityId)
                .Returns(Charity);
        }

        public void AndGiven_the_Charity_only_authenticates_on_premise()
        {
            Charity.AuthenticationType.Returns(AuthenticationType.OnPremise);
        }

        public void AndGiven_the_Charity_has_no_Administrator_matching_the_username_being_authenticated()
        {
            Charity.IsAdministrator(UserName).Returns(false);
        }

        public void When_authenticating_the_Charity_User()
        {
            SUT.Handle();
        }

        public void Then_the_user_should_be_authenticated_only_on_premise()
        {
            AuthenticationOnPremiseService
                .Received()
                .Login(SUT.Command.UserName, SUT.Command.Password, SUT.Command.RememberMe);

            AuthenticationOffPremiseService.DidNotReceive().Login(Arg.Any<string>(), Arg.Any<string>(),Arg.Any<bool>());
        }

    }
}