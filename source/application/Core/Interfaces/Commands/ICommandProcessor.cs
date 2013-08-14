namespace Intrigma.DonorSpace.Core.Interfaces.Commands
{
    public interface ICommandProcessor
    {
        IExecutionResult Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}