namespace Intrigma.DonorSpace.Core.Interfaces.Services
{
    public interface IAuthenticationOnPremiseService : IAuthenticationService
    {
        bool DeleteAccount(string userName);
        bool ChangeAccountWith(string oldUserName, string newUsername, 
                            string oldPassword, string newPassword);
    }
}