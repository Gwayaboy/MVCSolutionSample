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
    public class us002_sc02_Persisting_web_service_url 
        : us002_sc02<PostControllerScenario<AdministrationController,CharityAuthenticationDetailsForm>> 
    {
        private Charity _charity;
        const string ExpectedWebServiceUrl = "/some/web/service";
        private Charity PersistedCharity
        {
            get { return Database.Query<Charity>().Get(_charity.Id); }
        }

        public override void Given_the_Charity_authentication_type_is_off_premise()
        {
            _charity =
                Database.Save(Builder<Charity>
                                  .CreateNew()
                                  .With(c => c.AuthenticationType, AuthenticationType.OffPremise)
                                  .With(c => c.WebServiceUrl, string.Empty)
                                  .Build());
        }

        public override void AndGiven_a_valid_web_service_url_as_been_entered()
        {
            SUT.Form = new CharityAuthenticationDetailsForm
                {
                    Id = _charity.Id,
                    WebServiceUrl = ExpectedWebServiceUrl
                };
        }

        public override void When_saving_the_web_service_url()
        {
            SUT.ExecuteAction(x => x.UpdateAuthenticationDetails(SUT.Form));
        }
        
        public override void Then_the_web_service_url_should_be_persisted_against_the_relevant_Charity()
        {
            PersistedCharity.WebServiceUrl.Should().Be(ExpectedWebServiceUrl);
        }

        public override void AndThen_the_execution_result_should_be_successful()
        {
            SUT.ActionResult.Should().BeJsonResultOf<ExecutionResult>().And.Should().BeSuccessfull();
        }
    }
}