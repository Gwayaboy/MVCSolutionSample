using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication;
using Subtext.TestLibrary;

namespace Intrigma.DonorSpace.ExecutableSpecifications._001_User_authenfication
{
    public class us001_sc01_Authenticating_on_premise_Charity_Administrator : us001_sc01<PostControllerScenario<AccountController,AuthenticateUserForm>>
    {
        public override void Given_there_are_3_Charity_Administrators()
        {
            CreateAndSaveAdministratorAccount(AuthenticatedUserName, AuthenticatedPassword);
        }

        public override void AndGiven_the_user_about_to_be_authenticated_matches_the_administrator()
        {
            InitialiseHttpContext();
            SUT.Form = new AuthenticateUserForm
                {
                    CharityId = Charity.Id,
                    UserName = AuthenticatedUserName,
                    Password = AuthenticatedPassword,
                    RememberMe = true
                };
        }
        
        public override void When_the_user_is_authenticated()
        {
            SUT.ExecuteAction(x => x.Login(SUT.Form));
        }

        public override void Then_the_user_should_be_authenticated_on_premise()
        {
            SUT.ExecutionResult.Should().BeSuccessfull();
            UserIsAuthenticatedOnPremise.Should().BeTrue();
        }

        public override void AndThen_the_user_is_redirected_to_the_home_page()
        {
           SUT.ActionResult.Should().BeRedirectedToAction<HomeController>(c => c.Index(Charity.Id));
        }

        private static void InitialiseHttpContext()
        {
            new HttpSimulator().SimulateRequest();
        }

       
    }
}
