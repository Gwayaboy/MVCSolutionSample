using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Core.Commands.Administration
{
    public class EditAdministratorCommand : ICommand
    {
        public int Id { get; set; }

        public int CharityId { get; set; }

        public string OriginalUsername { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}