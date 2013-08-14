using System;
using System.Linq;
using AutoMapper;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping
{
    public class AutoMapperWrapper : IMapper
    {
        public IMappingConfiguration GetMappingConfigurationMatching(Type sourceType)
        {
            var typeMap = Mapper.GetAllTypeMaps().SingleOrDefault(m => m.SourceType == sourceType);

            return typeMap == null ? null : new TypeMapWrapper(typeMap);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}