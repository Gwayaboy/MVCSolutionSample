using Autofac;
using Intrigma.DonorSpace.Core.Interfaces.Services;
using Intrigma.DonorSpace.Services;
using NSubstitute;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class ApplicationServiceModule : Module
    {
        public const string FakeOffPremiseAuthorisedUserName = "userName";
        public const string FakeOffPremiseAuthorisedUserPassword = "passw0rd";
        private static IAuthenticationOffPremiseService _authenticationOffPremiseService;
        private IAuthenticationOffPremiseService FakeAuthenticationOffPremiseService
        {
            get
            {
                if (_authenticationOffPremiseService == null)
                {
                    _authenticationOffPremiseService = Substitute.For<IAuthenticationOffPremiseService>();
                    _authenticationOffPremiseService
                        .Login(FakeOffPremiseAuthorisedUserName,
                               FakeOffPremiseAuthorisedUserPassword,
                               Arg.Any<bool>())
                        .Returns(true);
                }
              

                return _authenticationOffPremiseService;
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebSecurityAuthenticationOnPremiseService>().As<IAuthenticationOnPremiseService>();
            builder.RegisterInstance(FakeAuthenticationOffPremiseService).As<IAuthenticationOffPremiseService>();
        }
    }
}