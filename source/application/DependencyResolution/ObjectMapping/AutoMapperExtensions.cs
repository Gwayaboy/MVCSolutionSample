using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Autofac;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping.Converters;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression,
                                                              Type sourceType, Type destinationType)
        {
            ApplyIgnoreOnNonExisting(property => expression.ForMember(property, opt => opt.Ignore()), sourceType,
                                     destinationType);

            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {

            ApplyIgnoreOnNonExisting(property => expression.ForMember(property, opt => opt.Ignore()),
                                     typeof(TSource),
                                     typeof(TDestination));
            return expression;
        }


        static void ApplyIgnoreOnNonExisting(Action<string> ignorePropertyAction, Type sourceType, Type destinationType)
        {
            var existingMap = 
                Mapper.GetAllTypeMaps()
                .First(x => x.SourceType == sourceType && x.DestinationType == destinationType);
            foreach (var property in existingMap.GetUnmappedPropertyNames())
            {
                ignorePropertyAction(property);
            }

        }

        public static Type ConcreteTypeImplementingTypeConverter(this IEnumerable<Type> withinTypes, Type sourceType, Type destinationType)
        {
            return
                withinTypes
                    .GetConcreteTypesImplementingOpenGenericInterfaceType(typeof (ITypeConverter<,>))
                    .Where(result => result.InterfaceType.GetGenericArguments()[0] == sourceType &&
                                     result.InterfaceType.GetGenericArguments()[1] == destinationType)
                    .Select(result => result.ConcreteType)
                    .SingleOrDefault();

        }

        public static void CreateMap(this IEnumerable<MapperExtensions.SourceAndDestinationTypes> mappingConfigurations)
        {
            foreach (var configuration in mappingConfigurations)
            {
                Mapper.CreateMap(configuration.SourceType, configuration.DestinationType)
                      .IgnoreAllNonExisting(configuration.SourceType, configuration.DestinationType);
            }
        }

        public static void CreateMapWithCustomTypeConverters(this IEnumerable<MapperExtensions.SourceAndDestinationTypes> mappingConfigurations)
        {
            foreach (var configuration in mappingConfigurations)
            {
                var customConverterType =
                    AssemblyScanner
                        .GetExportedTypesFromWebAssembly()
                        .ConcreteTypeImplementingTypeConverter(configuration.SourceType,
                                                               configuration.DestinationType);

                if (customConverterType != null)
                {
                    Mapper.CreateMap(configuration.SourceType, configuration.DestinationType)
                          .ConvertUsing(customConverterType);

                }
            }
        }
        
        public static void InitialiseAutoMapper(this IContainer container, Func<Type, object> serviceResolver = null)
        {
            Mapper.Initialize(x => x.ConstructServicesUsing(serviceResolver ?? DependencyResolver.Current.GetService));
            Mapper.CreateMap<Enum, string>().ConvertUsing(new EnumTypeConverter());
            Mapper.CreateMap<Entity, int>().ConvertUsing(new IdTypeConverterFromEntityWithTypedId<int>());
            
            using (var scope = container.BeginLifetimeScope())
            {
                foreach (var profile in scope.Resolve<IEnumerable<Profile>>())
                {
                    Mapper.AddProfile(profile);
                }
            }
        }
    }
}