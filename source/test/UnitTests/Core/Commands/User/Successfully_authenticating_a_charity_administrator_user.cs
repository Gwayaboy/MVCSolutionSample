using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Core.Commands.User
{
    public class Successfully_authenticating_a_charity_regular_user : AuthenticateUserCommandHandlerSpecification
    {
        public void Given_the_Charity_exists()
        {
            AuthenticationOnPremiseService
                .Login(SUT.Command.UserName, SUT.Command.Password, SUT.Command.RememberMe)
                .Returns(true);

            SubstituteFor<ICharityRepository>().Get(SUT.Command.CharityId).Returns(Charity);
       }

        public void AndGiven_the_Charity_has_an_Administrator_matching_the_username_being_authenticated()
        {
            Charity.IsAdministrator(SUT.Command.UserName).Returns(true);
        }

        public void When_authenticating_the_Charity_Admin()
        {
            SUT.Handle();
        }

        public void Then_the_user_should_be_authenticated_only_on_premise()
        {
            AuthenticationOnPremiseService.Received().Login(UserName, UserPassword,false);
            AuthenticationOffPremiseService.DidNotReceive().Login(UserName, UserPassword);
        }
    }
}
