using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication;

namespace Intrigma.DonorSpace.ExecutableSpecifications._001_User_authenfication
{
    public class us001_sc02_Authenticating_off_premise_Regular_Charity_User 
        : us001_sc02<PostControllerScenario<AccountController,AuthenticateUserForm>>
    {
        private Charity _charity;
        
        public override void Given_the_Charity_Authentication_type_is_off_Premise()
        {
            _charity = Builder<Charity>.CreateNew().With(c => c.AuthenticationType, AuthenticationType.OffPremise).Build();
            Database.Save(_charity);
        }

        public override void AndGiven_the_off_premise_authentication_service_authorises_the_user()
        {
            SUT.Form = new AuthenticateUserForm
                {
                    CharityId = _charity.Id,
                    UserName = ApplicationServiceModule.FakeOffPremiseAuthorisedUserName,
                    Password = ApplicationServiceModule.FakeOffPremiseAuthorisedUserPassword
                };
        }

        public override void When_the_user_is_authenticated()
        {
            SUT.ExecuteAction(c => c.Login(SUT.Form));
        }

        public override void Then_the_user_should_be_authenticated_off_premise()
        {
            SUT.ExecutionResult.Should().BeSuccessfull();
        }

        public override void AndThen_the_user_is_redirected_to_the_home_page()
        {
            SUT.ActionResult.Should().BeRedirectedToAction<HomeController>(c => c.Index(_charity.Id));
        }

    }
}