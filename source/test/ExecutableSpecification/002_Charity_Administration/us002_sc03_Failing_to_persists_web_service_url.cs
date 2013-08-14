using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Assertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._002_Charity_administration;

namespace Intrigma.DonorSpace.ExecutableSpecifications._002_Charity_Administration
{
    public class us002_sc03_Failing_to_persists_web_service_url :
        us002_sc03<PostControllerScenario<AdministrationController, CharityAuthenticationDetailsForm>>
    {
        private Charity _charity;
        private Charity CharityOutOfDatabase
        {
            get { return Database.Query<Charity>().Get(_charity.Id); }
        }

        const string ExistingWebserviceUrl = "/existing/webservice/url";


        public override void Given_the_Charity_authentication_type_is_off_premise()
        {
            _charity =
                Database.Save(Builder<Charity>
                                  .CreateNew()
                                  .With(c => c.AuthenticationType, AuthenticationType.OffPremise)
                                  .With(c => c.WebServiceUrl, ExistingWebserviceUrl)
                                  .Build());
        }

        public override void AndGiven_an_invalid_web_service_url_as_been_entered()
        {
            SUT.Form = new CharityAuthenticationDetailsForm
                {
                    Id = _charity.Id,
                    WebServiceUrl = string.Empty
                };
        }

        public override void When_saving_the_web_service_url()
        {
            SUT.ExecuteAction(x => x.UpdateAuthenticationDetails(SUT.Form));
        }

        public override void Then_the_web_service_url_should_be_not_be_persisted()
        {
            CharityOutOfDatabase.WebServiceUrl.Should().Be(ExistingWebserviceUrl);
        }
        
        public override void AndThen_the_execution_result_should_be_unsuccessfull()
        {
            SUT.ActionResult.Should().BeJsonResultOf<ExecutionResult>().And.Should().BeUnsuccessfull();
        }
    }
}