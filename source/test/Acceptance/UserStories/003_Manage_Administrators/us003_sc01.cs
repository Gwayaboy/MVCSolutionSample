using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._003_Manage_Administrators
{
    public abstract class us003_sc01<TScenarioContext> : ScenarioFor<TScenarioContext,us003_Manage_Administrators> 
        where TScenarioContext : class
    {
        protected us003_sc01() : base(01, "View all Administrators") { }

        public abstract void Given_the_Charity_has_5_Administrators();

        public abstract void When_all_Administrators_are_listed();

        public abstract void Then_there_should_be_5_Administrators();

        public abstract void AndThen_each_Administrator_should_have_the_corresponding_UserName_and_fullName();
    }
}