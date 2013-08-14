using System;
using System.Collections.Generic;
using System.Linq;

namespace Panzea.DonorSpace.Infrastructure.Extensions
{
    public static class MapperExtensions
    {
        public static Func<object, Type, Type, object> Map =
            (entity, entityType, dtoType) =>
                {
                    throw new InvalidOperationException(
                        "The Mapping function must be set");
                };

        public class SourceAndDestinationTypes
        {
            public Type SourceType { get; protected set; }
            public Type DestinationType { get; protected set; }

            public SourceAndDestinationTypes(Type sourceType, Type destinationType)
            {
                SourceType = sourceType;
                DestinationType = destinationType;
            }

        }

        public static IEnumerable<TViewModel> MapViewModel<TEntity, TViewModel>(this IEnumerable<TEntity> entities)
            where TEntity : class
            where TViewModel : class
        {
            return entities.Select(x => x.MapViewModel<TEntity, TViewModel>());
        }

        public static TViewModel MapViewModel<TEntity, TViewModel>(this TEntity entity)
            where TEntity : class
            where TViewModel : class
        {
            return Map(entity, typeof(TEntity), typeof(TViewModel)) as TViewModel;
        }





        public static IEnumerable<SourceAndDestinationTypes> GetSourceAndDestinationTypesFromMarkerMappingFromDomainInterface(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface)
        {
            return withinTypes.GetSourceAndDestinationTypesFor(genericMarkerMappingInterface);
        }

        public static IEnumerable<SourceAndDestinationTypes> GetSourceAndDestinationTypesFromMarkerMappingToCommandInterface(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface)
        {
            return withinTypes.GetSourceAndDestinationTypesFor(genericMarkerMappingInterface, swapDestinationAndSourceTypes: true);
        }

        private static IEnumerable<SourceAndDestinationTypes> GetSourceAndDestinationTypesFor(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface, Boolean swapDestinationAndSourceTypes = false)
        {
            return 
            withinTypes
                .GetConcreteTypesImplementingOpenGenericInterfaceType(genericMarkerMappingInterface)
                .Select(result => SourceAndDestinationTypesSelector(result,swapDestinationAndSourceTypes));
         

        }

        private static SourceAndDestinationTypes SourceAndDestinationTypesSelector(TypeExtensions.ConcreteTypeAndInterface concreteTypeAndInterfaces, Boolean swapDestinationAndSourceTypes = false)
        {
            if (swapDestinationAndSourceTypes)
            {
                return new SourceAndDestinationTypes(concreteTypeAndInterfaces.ConcreteType,
                                                     concreteTypeAndInterfaces.InterfaceType.GetGenericArguments()[0]);
            }
            return new SourceAndDestinationTypes(concreteTypeAndInterfaces.InterfaceType.GetGenericArguments()[0],concreteTypeAndInterfaces.ConcreteType);
        }
    }

   

}