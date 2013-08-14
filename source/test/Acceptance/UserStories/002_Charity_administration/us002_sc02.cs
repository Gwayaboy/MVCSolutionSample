using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._002_Charity_administration
{
    public abstract class us002_sc02<TScenarionContext> : ScenarioFor<TScenarionContext,us002_Charity_administration> 
        where TScenarionContext : class
    {
       protected us002_sc02() : base(2, "Persisting web site Url for Charity with off premise authentication") { }

        public abstract void Given_the_Charity_authentication_type_is_off_premise();

        public abstract void AndGiven_a_valid_web_service_url_as_been_entered();

        public abstract void When_saving_the_web_service_url();

        public abstract void Then_the_web_service_url_should_be_persisted_against_the_relevant_Charity();

        public abstract void AndThen_the_execution_result_should_be_successful();
    }
}