using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Intrigma.DonorSpace.Acceptance.Helper;
using Intrigma.DonorSpace.Acceptance.Modules;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Services;
using MvcContrib.TestHelper.Fakes;

namespace Intrigma.DonorSpace.Acceptance.Configuration
{
    /// <summary>
    /// Replicates the ApplicationConfigurator class from the web application. It differs from ApplicationConfigurator 
    /// </summary>
    public static class TestConfigurator
    {
        private static IContainer Container { get; set; }
        private const string ConnectionName = "ExecutableSpecifications";

        public static void SetUpTestEnvironement()
        {
            BuildContainer();

        }

        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            ConfigureAndRegisterDataModule(builder);
            ConfigureAndRegisterControllers(builder);
            RegisterModules(builder);

            Container = builder.Build();
            SetSpecificationScopeProvider();
            InitialiseDatabaseAndWebSecurity();
            Container.InitialiseAutoMapper(Container.BeginLifetimeScope().Resolve);
        }

        private static void SetSpecificationScopeProvider()
        {
            Specification.Specification.ScopeProvider = 
                () => new AutofacLifeTimeScope(Container.BeginLifetimeScope());
        }
        
        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<FormProcessorModule>();
            builder.RegisterModule<ScenarioModule>();
            builder.RegisterModule<CommandHandlerModule>();
            builder.RegisterModule<FluentValidationModule>();
            builder.RegisterModule<LoggerModule>();
            //builder.RegisterModule<StartUpModule>();
            builder.RegisterModule<AutoMapperModule>();
            builder.RegisterModule<ApplicationServiceModule>();

        }

       

       
        private static void InitialiseDatabaseAndWebSecurity()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var databaseInitialiser = scope.Resolve<IDatabaseInitialiser>();
                databaseInitialiser.Initialise(forceReinitialisation:true);
                ConnectionName.InitializeWebSecurityFor<Administrator>(x => x.UserName);
            }
      }

        private static void ConfigureAndRegisterDataModule(ContainerBuilder builder)
        {
            DataModule.ConnectionStringProvider = () => ConnectionName;
            DataModule.WithLifeStyle(c => c.InstancePerLifetimeScope());
            DataModule.WithModelStorageStrategy<AlwaysDropAndRecreateStorageModel>();

           builder.RegisterModule<DataModule>();


            builder.Register(c => new Database(c.Resolve<DataContext>(), c.Resolve<IDatabaseInitialiser>()))
                   .As<IDatabase>()
                   .InstancePerLifetimeScope();
        }

        private static void ConfigureAndRegisterControllers(ContainerBuilder builder)
        {
            ControllerModule.WithLifeStyle(c => c.InstancePerLifetimeScope());
            ControllerModule.WithUrlHelper(new UrlHelper(new RequestContext(FakeHttpContext.Root(), new RouteData()), RouteTable.Routes));
      
            //HttpContext.Current = new HttpContext(new HttpRequest(string.Empty,string.Empty,string.Empty),new HttpResponse(null));
            builder.RegisterModule<ControllerModule>();
        }

        public static void DisposeContainer()
        {
            Container.Dispose();
        }
    }
}