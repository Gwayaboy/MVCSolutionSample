using Panzea.DonorSpace.Core.Interfaces.Commands;

namespace Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IModelMapper
    {
        ICommand MapFormToCommand(object form);
    }
}