using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class CharityAuthenticationDetailsForm : IMapToCommand<CharityAuthenticationDetailsCommand>
    {
        public int Id { get; set; }
        public string WebServiceUrl { get; set; }
    }
}