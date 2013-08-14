using Autofac;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Commands;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class CommandProcessorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>();


        }
    }
}