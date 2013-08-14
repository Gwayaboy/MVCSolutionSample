using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class EditAdministratorForm : IMapFromDomain<Administrator>,IMapToCommand<EditAdministratorCommand>
    {
        public int Id { get; set; }

        public int CharityId { get; set; }

        [HiddenInput]
        public string OriginalUserName { get; set; }

        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Full name")]
        public string FullName { get; set; }

        [DisplayName("Current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("new password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}