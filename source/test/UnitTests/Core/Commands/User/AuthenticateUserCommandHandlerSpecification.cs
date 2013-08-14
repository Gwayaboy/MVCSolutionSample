using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.Commands.User;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Services;

namespace Intrigma.DonorSpace.UnitTests.Core.Commands.User
{
    public abstract class AuthenticateUserCommandHandlerSpecification : SpecificationFor<AuthenticateUserCommandHandler>
    {
        protected const string UserName = "some user";
        protected const string UserPassword = "#$%$%&**&";
        protected readonly IAuthenticationOnPremiseService AuthenticationOnPremiseService;
        protected readonly IAuthenticationOffPremiseService AuthenticationOffPremiseService;
        protected readonly Charity Charity;

        protected AuthenticateUserCommandHandlerSpecification()
        {
            Charity = SubstituteFor<Charity>();
            AuthenticationOnPremiseService = SubstituteFor<IAuthenticationOnPremiseService>();
            AuthenticationOffPremiseService = SubstituteFor<IAuthenticationOffPremiseService>();
            
            SUT.Command = new AuthenticateUserCommand
                {
                    CharityId = 5,
                    UserName = UserName,
                    Password = UserPassword
                };
        }
    }
}