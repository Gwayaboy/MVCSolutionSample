using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Core.Interfaces.Services;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    public class EditAdministratorCommandHandler : CommandHandler<EditAdministratorCommand>
    {
        private readonly ICharityRepository _charityRepository;
        private readonly IAuthenticationOnPremiseService _authenticationOnPremise;
        
        public EditAdministratorCommandHandler(ICharityRepository charityRepository,
                                               IAuthenticationOnPremiseService authenticationOnPremise)
        {
            _charityRepository = charityRepository;
            _authenticationOnPremise = authenticationOnPremise;
        }

        public override void Handle()
        {

            var charity = _charityRepository.Get(Command.CharityId);
            if (charity == null) throw BusinessException("The specified charity is unknown");

            charity.UpdateAdministrator(Command);

            Commit(() => _charityRepository.Save(charity));

            ChangePassWord();

        }

        private void ChangePassWord()
        {
            var passWordSuccessfullChanged =
                _authenticationOnPremise
                    .ChangeAccountWith(Command.OriginalUsername,
                                       Command.Username,
                                       Command.CurrentPassword,
                                       Command.NewPassword);

            if (!passWordSuccessfullChanged)
            {
                throw BusinessExceptionFor(c => c.CurrentPassword,
                                           withMessage: "The specified current password doesn't match");
            }

        }

       
    }
}