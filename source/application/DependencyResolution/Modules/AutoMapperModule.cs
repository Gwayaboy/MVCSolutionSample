using System;
using System.Security.Principal;
using AutoMapper;
using Autofac;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class AutoMapperModule : Module
    {
       protected override void Load(ContainerBuilder builder)
        {
            RegisterMapper(builder);
            RegisterProfiles(builder);
        }

        private void RegisterProfiles(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsSubClassOf<Profile>())
                .As<Profile>();
        }
        
        private void RegisterMapper(ContainerBuilder builder)
        {
            builder.RegisterType<AutoMapperWrapper>().As<IMapper>();
            builder.Register(c => new ModelMapper(c.Resolve<IMapper>())).As<IModelMapper>();
            builder.RegisterType<ModelMapper>().As<IModelMapper>();

          
            builder.RegisterAssemblyTypes(AssemblyScanner.FromWebAssembly())
                   .Where(t => t.IsConcreteTypeOf<IHaveCustomMappings>())
                   .AsSelf()
                   .OnActivating(e =>
                       {
                           var viewModel = (IHaveCustomMappings) e.Instance;
                           viewModel.UserIdentityProvider = e.Context.Resolve<Func<IPrincipal>>();
                       });

        }
    }
}