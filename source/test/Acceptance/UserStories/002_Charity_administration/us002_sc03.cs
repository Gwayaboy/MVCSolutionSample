using Intrigma.DonorSpace.Acceptance.Specification;

namespace Intrigma.DonorSpace.Acceptance.UserStories._002_Charity_administration
{
    public abstract class us002_sc03<TScenarionContext> : ScenarioFor<TScenarionContext, us002_Charity_administration>
        where TScenarionContext : class
    {
        protected us002_sc03() : base(3, "Failing to persist web site Url") { }

        public abstract void Given_the_Charity_authentication_type_is_off_premise();

        public abstract void AndGiven_an_invalid_web_service_url_as_been_entered();

        public abstract void When_saving_the_web_service_url();

        public abstract void Then_the_web_service_url_should_be_not_be_persisted();

        public abstract void AndThen_the_execution_result_should_be_unsuccessfull();
    }
}