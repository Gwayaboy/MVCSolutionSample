using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._002_Charity_administration;


namespace Intrigma.DonorSpace.ExecutableSpecifications._002_Charity_Administration
{
    public class us002_sc01_Prepopulating_Charity_Administration 
        : us002_sc01<AutoMappedViewControllerScenario<AdministrationController,CharityViewModel>>
    {
        private ISingleObjectBuilder<Charity> _charityBuilder;
        private Charity _charity;

        public override void Given_an_existing_Charity_has_a_name_and_a_description()
        {
            _charityBuilder =
                Builder<Charity>
                    .CreateNew()
                    .With(c => c.SiteName, "My Charity")
                    .With(c => c.WebServiceUrl,"/some/url")
                    .With(c => c.Description, "for the good causes");
        }

        public override void AndGiven_the_Charity_has_an_off_premise_authentication_type()
        {
            _charity = _charityBuilder.With(x => x.AuthenticationType, AuthenticationType.OffPremise).Build();
            Database.Save(_charity);
        }

        public override void When_displaying_the_Charity_Administration_page()
        {
            SUT.ExecuteAction(x => x.Index(_charity.Id));
        }

        public override void Then_the_page_should_be_prepopulated_with_existing_details()
        {
            SUT.ViewModel.ShouldBeEquivalentTo(_charity);
        }
    }
}