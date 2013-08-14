using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Core.Interfaces.Services;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    public class DeleteAdministratorCommandHandler : CommandHandler<DeleteAdministratorCommand>
    {
        private readonly ICharityRepository _charityRepository;
        private readonly IAuthenticationOnPremiseService _onPremiseService;

        public DeleteAdministratorCommandHandler(ICharityRepository charityRepository,
                                                 IAuthenticationOnPremiseService onPremiseService)
        {
            _charityRepository = charityRepository;
            _onPremiseService = onPremiseService;
        }

        public override void Handle()
        {
            var charity = _charityRepository.Get(Command.CharityId);
            if (charity == null) throw BusinessException("The specified charity is unknown");

            charity.RemoveAdministrator(Command.UserName);
            
            Commit(() => _charityRepository.Save(charity));

            DeleteUserAccount();
        }
      
        private void DeleteUserAccount()
        {
            var isDeleted = _onPremiseService.DeleteAccount(Command.UserName);
            if (!isDeleted)
            {
                var inexistantUserErrorMessage = string.Format("The username {0} did not exist", Command.UserName);
                throw BusinessExceptionFor(c => c.UserName, inexistantUserErrorMessage);
            }
        }
    }
}