using System;
using System.Security.Principal;
using System.Web;
using Autofac;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class UserIdentityModule : Module
    {
        private static Func<IPrincipal> _userProvider = () => HttpContext.Current.User;

        public static void WithUser(Func<IPrincipal> userProvider)
        {
            _userProvider = userProvider;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => _userProvider);
        }
    }
}