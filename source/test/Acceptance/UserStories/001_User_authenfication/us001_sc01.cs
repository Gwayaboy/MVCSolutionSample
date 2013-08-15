using FizzWare.NBuilder;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.Domain;
using WebMatrix.WebData;

using System.Web.Security;

namespace Intrigma.DonorSpace.Acceptance.UserStories._001_User_authenfication
{
    public abstract class us001_sc01<TScenarioContext> 
        : ScenarioFor<TScenarioContext,us001_User_authentication> where TScenarioContext : class
    {

        protected const string AuthenticatedUserName = "Me";
        protected const string AuthenticatedPassword = "Passw0rd";
        protected readonly Charity Charity = new Charity();



        protected bool UserIsAuthenticatedOnPremise
        {
            get
            {
                var memberShip = Membership.GetUser(AuthenticatedUserName);
                return memberShip != null && memberShip.IsApproved;
            }
        }

        protected void CreateAndSaveAdministratorAccount(string authenticatedUserName, string password)
        {
            var administrator =
                Builder<Administrator>.CreateNew().With(x => x.UserName, authenticatedUserName).Build();
            Charity.AddAdministrators(administrator);

            Database.Repository<Charity>().Save(Charity);

            WebSecurity.CreateAccount(authenticatedUserName, password);
        }
        
        
        protected us001_sc01() : base(1, "Authenticating on premise Charity Administrator") { }

        public abstract void Given_there_are_3_Charity_Administrators();
     
        public abstract void AndGiven_the_user_about_to_be_authenticated_matches_the_administrator();

        public abstract void When_the_user_is_authenticated();

        public abstract void Then_the_user_should_be_authenticated_on_premise();

        public abstract void AndThen_the_user_is_redirected_to_the_home_page();

    }
}
