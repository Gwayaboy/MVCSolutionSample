using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication
{
    public abstract class us001_sc02<TScenarioContext>
        : ScenarioFor<TScenarioContext, us001_User_authentication> where TScenarioContext : class
    {

        protected us001_sc02() : base(2, "Authenticating off premise Regular Charity User") { }


        public abstract void Given_the_Charity_Authentication_type_is_off_Premise();

        public abstract void AndGiven_the_off_premise_authentication_service_authorises_the_user();

        public abstract void When_the_user_is_authenticated();

        public abstract void Then_the_user_should_be_authenticated_off_premise();

        public abstract void AndThen_the_user_is_redirected_to_the_home_page();
    }
}