using Intrigma.DonorSpace.Core.Interfaces.Data.Write;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    public class CharityAuthenticationDetailsCommandHandler : CommandHandler<CharityAuthenticationDetailsCommand>
    {
        private readonly ICharityRepository _charityRepository;

        public CharityAuthenticationDetailsCommandHandler(ICharityRepository charityRepository)
        {
            _charityRepository = charityRepository;
        }

        public override void Handle()
        {
            var charity = _charityRepository.Get(Command.Id);
            if (charity == null) throw BusinessException("The specified charity is unknown");

            charity.UpdateAuthenticationDetails(Command.WebServiceUrl);

            _charityRepository.Save(charity);
        }
    }
}