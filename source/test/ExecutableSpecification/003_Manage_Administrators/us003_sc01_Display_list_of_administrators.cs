using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Web.Controllers;
using Intrigma.DonorSpace.Web.ViewModels;
using Intrigma.DonorSpace.Acceptance.UserStories._003_Manage_Administrators;

namespace Intrigma.DonorSpace.ExecutableSpecifications._003_Manage_Administrators
{
    public class us003_sc01_Display_list_of_administrators
        : us003_sc01<AutoMappedViewControllerScenario<AdministrationController,IEnumerable<AdministratorDetailViewModel>>>
    {
        readonly Charity _charity = new Charity();

        private IEnumerable<AdministratorDetailViewModel> AllAdministratorDetails
        {
            get { return SUT.ViewModel; }
        }

        private Expression<Func<AdministratorDetailViewModel,bool>> AdministratorDetailsWithNonEmptyIdUserNameAndFullName
        {
            get
            {
                return viewModel => !string.IsNullOrWhiteSpace(viewModel.FullName) &&
                                    !string.IsNullOrWhiteSpace(viewModel.UserName) &&
                                    viewModel.Id > 0;
            }
        }

        public override void Given_the_Charity_has_5_Administrators()
        {
            _charity.AddAdministrators(Builder<Administrator>.CreateListOfSize(5).Build().ToArray());
            Database.Save(_charity);
        }

        public override void When_all_Administrators_are_listed()
        {
            SUT.ExecuteAction(c => c.Administrators(_charity.Id));
        }

        public override void Then_there_should_be_5_Administrators()
        {
            AllAdministratorDetails.Should().HaveCount(5);
        }

        public override void AndThen_each_Administrator_should_have_the_corresponding_UserName_and_fullName()
        {
            AllAdministratorDetails.Should().OnlyContain(AdministratorDetailsWithNonEmptyIdUserNameAndFullName);
            AllAdministratorDetails.ShouldBeEquivalentTo(_charity.Administrators,c => c.ExcludingMissingProperties());
        }
    }
}