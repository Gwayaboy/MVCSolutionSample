using Panzea.DonorSpace.Core.Interfaces.Commands;

namespace Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IHaveCustomMappingToCommand<T> where T : ICommand
    {
    }
}