using System;
using System.Linq.Expressions;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using WebMatrix.WebData;

namespace Intrigma.DonorSpace.Services
{
    public static class WebSecurityExtensions
    {
        public static void InitializeWebSecurityFor<T>(this string connectionName, Expression<Func<T, string>> userNamePropertySelector)
            where T : Entity
        {
            if (WebSecurity.Initialized) return;

            var userNameColumn = userNamePropertySelector.GetPropertyFromLambda().Name;
                
            WebSecurity.InitializeDatabaseConnection(connectionName, typeof (T).Name + "s", "Id", userNameColumn,
                                                     autoCreateTables: false);
        }

        public static void CreateAccount(this String userName, string passWord, bool requireConfirmationToken = false)
        {
            if (WebSecurity.UserExists(userName))
            {
                WebSecurity.CreateAccount(userName, passWord, requireConfirmationToken);
            }
        }

        
    }
}