using Autofac;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class StartUpModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsConcreteTypeOf<IStartable>())
                .As<IStartable>()
                .SingleInstance();
        }
    }
}