using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Core.Commands.User
{
    public class AuthenticateUserCommand : ICommand
    {
        public int CharityId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}