using Panzea.DonorSpace.Core.Domain.Base;

namespace Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IMapFromDomain<T>
        where T: Entity
    {
    }
}