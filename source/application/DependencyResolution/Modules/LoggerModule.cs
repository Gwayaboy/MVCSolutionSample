using Autofac;
using Intrigma.DonorSpace.Infrastructure.Logging;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(NullLogger.Instance).As<ILogger>().SingleInstance();
        }
    }
}