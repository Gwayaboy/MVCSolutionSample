using Autofac;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Logging;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class FormProcessorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new FormProcessor(c.Resolve<ILogger>(),
                                                 c.Resolve<IModelMapper>(),
                                                 c.Resolve<ICommandProcessor>()))
                .As<IFormProcessor>();

        }


    }
}