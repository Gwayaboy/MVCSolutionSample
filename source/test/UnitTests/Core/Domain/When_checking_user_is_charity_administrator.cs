using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.Domain;

namespace Intrigma.DonorSpace.UnitTests.Core.Domain
{
    public class When_checking_user_is_charity_administrator : SpecificationFor<Charity>
    {
        private bool _isAdministrator;
        const string UserName = "someuserName";
        private IListBuilder<Administrator> _administratorListBuilder; 
        
        public void Given_there_are_many_administrators()
        {
            _administratorListBuilder = Builder<Administrator>.CreateListOfSize(3);
        }

        public void AndGiven_the_last_administrator_matches_the_requested_user_name()
        {
            var lastAdministratorHasSameUserName =
                _administratorListBuilder
                        .TheLast(1)
                        .With(x => x.UserName, UserName)
                    .Build()
                    .ToArray();

            SUT.AddAdministrators(lastAdministratorHasSameUserName);
        }

        public void When_checking_user_is_administrator()
        {
            _isAdministrator = SUT.IsAdministrator(UserName);
        }

        public void Then_the_result_should_be_true()
        {
            _isAdministrator.Should().BeTrue();
        }
    }
}
