using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._002_Charity_administration
{
    public abstract class us002_sc01<TScenarionContext> : ScenarioFor<TScenarionContext,us002_Charity_administration> 
        where TScenarionContext : class
    {
        protected us002_sc01() : base(1, "Prepopulating a Charity Administration details") { }

        public abstract void Given_an_existing_Charity_has_a_name_and_a_description();

        public abstract void AndGiven_the_Charity_has_an_off_premise_authentication_type();

        public abstract void When_displaying_the_Charity_Administration_page();

        public abstract void Then_the_page_should_be_prepopulated_with_existing_details();
    }
}