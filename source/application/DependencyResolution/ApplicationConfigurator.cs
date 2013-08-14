using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Services;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution
{
    public static class ApplicationConfigurator
    {
        private static IContainer Container { get; set; }
        private static Assembly ThisAssembly { get { return typeof (ApplicationConfigurator).Assembly; } }

        public static void Bootstrap()
        {
            BuildContainer();
            Start();
        }

        private static void BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(ThisAssembly);
            Container = builder.Build();
        }

        private static void Start()
        {
            SetDependencyResolver();
            Container.InitialiseAutoMapper();
            InitialiseWebSecurity();
        }

        private static void InitialiseWebSecurity()
        {
            DependencyResolver
                .Current
                .GetService<IDatabaseInitialiser>()
                .Initialise(forceReinitialisation: true);

            DataModule
                .ConnectionStringProvider()
                .InitializeWebSecurityFor<Administrator>(x => x.UserName);
        }

        public static void SetDependencyResolver()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}