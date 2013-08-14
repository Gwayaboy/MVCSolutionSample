using Autofac;
using Panzea.DonorSpace.Core.Interfaces.Commands;

namespace Panzea.DonorSpace.Infrastructure.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IContainer _container;

        public CommandHandlerFactory(IContainer container)
        {
            _container = container;
        }

        public ICommandHandler HandlerForCommand(ICommand command)
        {
            var closedCommandHandlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            return (ICommandHandler) _container.Resolve(closedCommandHandlerType);

        }
    }
}