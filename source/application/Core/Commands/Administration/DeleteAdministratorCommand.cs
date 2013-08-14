using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    
    public class DeleteAdministratorCommand : ICommand
    {
        public int CharityId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}