using Autofac;
using Intrigma.DonorSpace.Core.Commands;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class CommandHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterDynamicFactoryFor<ICommandHandler>();
            RegisterCommandHandlers(builder);

            builder.RegisterModule<CommandProcessorModule>();
        }

         public static void RegisterCommandHandlers(ContainerBuilder builder)
         {

             var openGenericIntefaceType = typeof (ICommandHandler<>);

            var resultTypes =
                AssemblyScanner
                .FromAssemblyContaining(typeof (CommandHandler<>))
                    .GetExportedTypes()
                    .GetClosedTypesOf(openGenericIntefaceType);

            foreach (var resultType in resultTypes)
            {
                var commandType = resultType.InterfaceType.GetGenericArguments()[0];
                builder.RegisterType(resultType.ConcreteType).Keyed<ICommandHandler>(commandType);
            }
        }
        
    }


}