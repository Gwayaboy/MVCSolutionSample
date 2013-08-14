using System.Web.Security;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Services;
using WebMatrix.WebData;

namespace Intrigma.DonorSpace.Services
{
    public class WebSecurityAuthenticationOnPremiseService : IAuthenticationOnPremiseService
    {
        public bool Login(string userName, string passWord, bool rememberMe)
        {
            return WebSecurity.Login(userName, passWord, rememberMe);
        }
        
        public bool ChangeAccountWith(string oldUserName, string newUsername,string oldPassword,  string newPassword)
        {
            
            var passwordHasSuccessfullyChanged = WebSecurity.ChangePassword(newUsername, oldPassword, newPassword);

            if (!passwordHasSuccessfullyChanged) return false;

            var userNameHasChanged = oldUserName != newUsername;

            return !userNameHasChanged || RemoveOldAccountAndLoginToNew(oldUserName, newUsername, newPassword);
        }

        private bool RemoveOldAccountAndLoginToNew(string oldUserName, string newUsername, string newPassword)
        {
            try
            {
                WebSecurity.Logout();
                DeleteAccount(oldUserName);
                return WebSecurity.Login(newUsername, newPassword, true);
            }
            catch
            {
                throw new BusinessRuleException("An error occured while re-login");
            }
           
        }

        public bool DeleteAccount(string userName)
        {
            if (WebSecurity.UserExists(userName))
            {
                RemoveAllAssociatedRolesForUser(userName);
                return Membership.DeleteUser(userName);
            }
            return true;
        }

        private static void RemoveAllAssociatedRolesForUser(string userName)
        {
       
            foreach (var role in Roles.GetRolesForUser(userName))
            {
                Roles.RemoveUserFromRole(userName, role);
            }
        }
    }
}