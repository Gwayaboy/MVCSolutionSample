using System.ComponentModel.DataAnnotations;
using Intrigma.DonorSpace.Core.Commands.User;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class AuthenticateUserForm : IMapToCommand<AuthenticateUserCommand>
    {
        public int CharityId { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}