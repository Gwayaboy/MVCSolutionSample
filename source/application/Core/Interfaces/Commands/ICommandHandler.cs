using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Core.Interfaces.Commands
{
    public interface ICommandHandler
    {
        void Handle();
        IUnitOfWork UnitOfWork { get; set; }

    }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class, ICommand
    {
        TCommand Command { get; set; }
    }
}