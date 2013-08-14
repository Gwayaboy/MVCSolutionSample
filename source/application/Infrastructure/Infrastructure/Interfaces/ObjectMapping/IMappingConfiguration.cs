using System;

namespace Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IMappingConfiguration
    {
        Type DestinationType { get; }
    }
}