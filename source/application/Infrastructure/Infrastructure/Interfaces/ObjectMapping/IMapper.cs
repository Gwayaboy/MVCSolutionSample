using System;

namespace Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IMapper
    {
        IMappingConfiguration GetMappingConfigurationMatching(Type sourceType);

        object Map(object source, Type sourceType, Type destinationType);

        TDestination Map<TSource, TDestination>(TSource source);
    }
}