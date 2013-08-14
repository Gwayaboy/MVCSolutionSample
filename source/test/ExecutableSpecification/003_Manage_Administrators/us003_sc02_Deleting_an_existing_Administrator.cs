using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._003_Manage_Administrators;

namespace Intrigma.DonorSpace.ExecutableSpecifications._003_Manage_Administrators
{
    public class us003_sc02_Deleting_an_existing_Administrator : 
        us003_sc02<PostControllerScenario<AdministrationController,DeleteAdministratorForm>>
    {
        private readonly Charity _charity = new Charity();
        private string _administratorUserNameToBeDeleted;

        private bool DeletedAdministratorExistsInDatabase
        {
            get { return Database.Query<Administrator>().Exists(a => a.UserName == _administratorUserNameToBeDeleted); }
        }
        private IEnumerable<Administrator> RemainingAdministrators
        {
            get { return Database.Query<Charity>().Get(_charity.Id).Administrators; }
        }
        
        public override void Given_the_Charity_has_3_Administrators()
        {
            CreateCharityAndThreeAdministrators();
            SUT.Form = new DeleteAdministratorForm
                {
                    CharityId = _charity.Id,
                    UserName = _administratorUserNameToBeDeleted
                };
        }
        
        public override void When_Deleting_the_last_Administrator()
        {
            SUT.ExecuteAction(x => x.DeleteAdministrator(SUT.Form));
        }

        public override void Then_the_relevant_Administration_should_have_been_deleted()
        {
            DeletedAdministratorExistsInDatabase.Should().BeFalse();
        }
        
        public override void AndThen_there_should_be_2_Administrators_remaining()
        {
            RemainingAdministrators.Should().HaveCount(2);
        }

        private void CreateCharityAndThreeAdministrators()
        {
            var administrators = Builder<Administrator>.CreateListOfSize(3).Build().ToArray();
            _charity.AddAdministrators(administrators);
            Database.Save(_charity);

            _administratorUserNameToBeDeleted = administrators.Last().UserName;
        }
    }
}