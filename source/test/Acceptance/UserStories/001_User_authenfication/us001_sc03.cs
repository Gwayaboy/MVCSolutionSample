using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication
{
    public abstract class us001_sc03<TScenarioContext>
        : ScenarioFor<TScenarioContext, us001_User_authentication> where TScenarioContext : class
    {
        protected us001_sc03() : base(3, "Failing to authenticate off premise unknown User") { }

        public abstract void Given_the_Charity_Authentication_type_is_off_Premise();

        public abstract void AndGiven_the_on_premise_authentication_service_does_not_authorise_the_User();

        public abstract void When_the_user_is_authenticated();

        public abstract void Then_the_user_should_not_be_authenticated_off_premise();

        public abstract void AndThen_there_should_be_an_error_message_for_the_user_name();

        public abstract void AndThen_the_user_stays_on_the_login_page();

    }
}