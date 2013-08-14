using System.ComponentModel;
using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class AddAdministratorForm : IMapToCommand<AddAdministratorCommand>
    {
        [HiddenInput]
        public int CharityId { get; set; }

        [DisplayName("User name (email address)")]
        public string UserName { get; set; }

        [DisplayName("Full name")]
        public string FullName { get; set; }

        public string Password { get; set; }

        [DisplayName("Confirmed Password")]
        public string ConfirmedPassword { get; set; }
    }
}