using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    /// <summary>
    /// Marker interface to the command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapToCommand<T> where T : ICommand
    {
    }
}