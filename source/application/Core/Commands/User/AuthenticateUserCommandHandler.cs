using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Core.Interfaces.Services;

namespace Intrigma.DonorSpace.Core.Commands.User
{
    public class AuthenticateUserCommandHandler : CommandHandler<AuthenticateUserCommand>
    {
        private readonly ICharityRepository _charityRepository;
        private readonly IAuthenticationOnPremiseService _authenticationOnPremiseService;
        private readonly IAuthenticationOffPremiseService _authenticationOffPremiseService;


        public AuthenticateUserCommandHandler(ICharityRepository charityRepository,
                                              IAuthenticationOnPremiseService authenticationOnPremiseService,
                                              IAuthenticationOffPremiseService authenticationOffPremiseService)
        {
            _charityRepository = charityRepository;
            _authenticationOnPremiseService = authenticationOnPremiseService;
            _authenticationOffPremiseService = authenticationOffPremiseService;
        }

        public override void Handle()
        {
            var charity = _charityRepository.Get(Command.CharityId);
            if (charity == null) throw BusinessException("The specified charity is unknown");


            bool isAuthenticated;

            if (charity.IsAdministrator(Command.UserName) || charity.AuthenticationType == AuthenticationType.OnPremise)
            {
                isAuthenticated = _authenticationOnPremiseService.Login(Command.UserName, Command.Password, Command.RememberMe);
            }
            else
            {
                isAuthenticated = _authenticationOffPremiseService.Login(Command.UserName, Command.Password, Command.RememberMe);
            }

            if (!isAuthenticated)
            {
                var userFailedToAuthenticateMessage = string.Format("User {0} has failed to authenticate", Command.UserName);
                throw BusinessException(userFailedToAuthenticateMessage, forAllProperties: true);
            }
        }

        
    }
}