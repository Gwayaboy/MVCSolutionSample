using System;

namespace Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IMappingConfiguration
    {
        Type DestinationType { get; }
    }
}