using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    public class CharityAuthenticationDetailsCommand : ICommand
    {
        public int Id { get; set; }
        public string WebServiceUrl { get; set; }
    }
}