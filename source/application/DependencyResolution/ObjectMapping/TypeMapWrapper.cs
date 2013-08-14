using System;
using AutoMapper;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping
{
    public class TypeMapWrapper : IMappingConfiguration
    {
        public TypeMapWrapper(TypeMap typeMap)
        {
            DestinationType = typeMap.DestinationType;
        }
        
        public Type DestinationType { get; private set; }
    }
}