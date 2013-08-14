using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class DeleteAdministratorForm : IMapToCommand<DeleteAdministratorCommand>
    {
        public int CharityId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}