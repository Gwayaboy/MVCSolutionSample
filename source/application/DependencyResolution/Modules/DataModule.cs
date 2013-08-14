using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Initialisation;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Module = Autofac.Module;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class DataModule : Module
    {
        public static Func<string> ConnectionStringProvider = () => "DefaultConnection";
        
        private static Assembly DataAssembly = AssemblyScanner.FromAssemblyContaining<DataContext>();

        private static Action<IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle>>
            _lifeStyleProvider = x => x.InstancePerHttpRequest();

        private static Type _modelStorageStrategyInitialiserType = typeof (DropAndRecreateWhenModelStorageChanges);


        public static void WithLifeStyle(Action<IRegistrationBuilder<Object, SimpleActivatorData, SingleRegistrationStyle>> dataContextLifeStyleProvider)
        {
            _lifeStyleProvider = dataContextLifeStyleProvider;
        }
        

        public static void WithModelStorageStrategy<T>()
            where T:IModelStorageStrategyInitialiser
        {
            _modelStorageStrategyInitialiserType = typeof (T);
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMappingConfigurationTypeFindersAndDataContext(builder);

            RegisterDataContextPopulators(builder);

            RegisterDatabaseInitialisers(builder);

            RegisterUnitOfWorkAndRepositories(builder);
        }

        private static void RegisterUnitOfWorkAndRepositories(ContainerBuilder builder)
        {
            builder
                .Register(c => new UnitOfWork(c.Resolve<DataContext>()))
                .As<IUnitOfWork>()
                .ConfigureWith(_lifeStyleProvider);

            builder.RegisterModule<QueryAndRepositoryModule>();
        }

        private static void RegisterDatabaseInitialisers(ContainerBuilder builder)
        {
            builder.RegisterType(_modelStorageStrategyInitialiserType).As<IModelStorageStrategyInitialiser>();

            builder
                .Register(
                    c =>
                    new DatabaseInitialiser(c.Resolve<IModelStorageStrategyInitialiser>(),
                                            c.Resolve<DataContext>().Database))
                .As<IDatabaseInitialiser>()
                .ConfigureWith(_lifeStyleProvider);
        }

        private static void RegisterDataContextPopulators(ContainerBuilder builder)
        {
            builder.RegisterType<PopulateDataContextHandler>()
                   .As<IPopulateDataHandler<DataContext>>();

            builder.Register(c => ConnectionStringProvider).AsSelf();

            builder
                .RegisterAssemblyTypes(DataAssembly)
                .Where(t => t.IsConcreteTypeOf<IPopulate<DataContext>>())
                .As<IPopulate<DataContext>>();
        }

        private static void RegisterMappingConfigurationTypeFindersAndDataContext(ContainerBuilder builder)
        {

            builder.RegisterInstance(new DefaultMappingConfigurationTypesFinder())
                   .As<MappingConfigurationTypesFinder>()
                   .AsSelf()
                   .SingleInstance();

            builder.Register(c => c.Resolve<DefaultMappingConfigurationTypesFinder>().MappingConfigurationTypes)
                   .AsSelf();

            builder
                .RegisterAssemblyTypes(DataAssembly)
                .Where(t => t.IsConcreteTypeOf<MappingConfigurationTypesFinder>() && 
                            t != typeof(DefaultMappingConfigurationTypesFinder))
                .As<MappingConfigurationTypesFinder>();

            builder
                .Register(c => new DataContextFactory(c.Resolve<IEnumerable<MappingConfigurationTypesFinder>>(),
                                                      ConnectionStringProvider()))
                .As<IDataContextFactory>()
                .SingleInstance();

            builder
                .Register(c => c.Resolve<IDataContextFactory>().Create())
                .ConfigureWith(_lifeStyleProvider);
        }
    }
}