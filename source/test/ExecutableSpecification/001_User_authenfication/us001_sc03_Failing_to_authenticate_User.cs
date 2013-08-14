using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication;

namespace Intrigma.DonorSpace.ExecutableSpecifications._001_User_authenfication
{
    public class us001_sc03_Failing_to_authenticate_User : 
        us001_sc03<PostControllerScenario<AccountController, AuthenticateUserForm>>
    {
        private Charity _charity;

        public override void Given_the_Charity_Authentication_type_is_off_Premise()
        {
            _charity =
                Builder<Charity>
                    .CreateNew()
                    .With(c => c.AuthenticationType, AuthenticationType.OffPremise)
                    .Build();

            Database.Save(_charity);
        }

        public override void AndGiven_the_on_premise_authentication_service_does_not_authorise_the_User()
        {
            SUT.Form = new AuthenticateUserForm
                {
                    CharityId = _charity.Id,
                    UserName = "userName",
                    Password = "badPassw0rd"
                };
        }

        public override void When_the_user_is_authenticated()
        {
            SUT.ExecuteAction(x => x.Login(SUT.Form));
        }

        public override void Then_the_user_should_not_be_authenticated_off_premise()
        {
            SUT.ExecutionResult.Should().BeUnsuccessfull();
        }

        public override void AndThen_there_should_be_an_error_message_for_the_user_name()
        {
            SUT.ExecutionResult.Should().HaveErrorMessage("User userName has failed to authenticate");
        }

        public override void AndThen_the_user_stays_on_the_login_page()
        {
            SUT.ActionResult.Should().BeDefaultViewForAction();
        }
    }
}