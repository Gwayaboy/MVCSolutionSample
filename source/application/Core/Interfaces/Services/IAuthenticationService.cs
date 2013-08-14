namespace Intrigma.DonorSpace.Core.Interfaces.Services
{
    public interface  IAuthenticationService
    {
        bool Login(string userName, string passWord, bool rememberMe = true);
    }
}