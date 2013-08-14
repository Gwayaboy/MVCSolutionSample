using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._003_Manage_Administrators
{
    public abstract class us003_sc02<TScenarioContext> : ScenarioFor<TScenarioContext, us003_Manage_Administrators>
        where TScenarioContext : class
    {
        protected us003_sc02() : base(2, "Deleting an existing Administrator") { }

        public abstract void Given_the_Charity_has_3_Administrators();

        public abstract void When_Deleting_the_last_Administrator();

        public abstract void Then_the_relevant_Administration_should_have_been_deleted();
     
        public abstract void AndThen_there_should_be_2_Administrators_remaining();

    }
}