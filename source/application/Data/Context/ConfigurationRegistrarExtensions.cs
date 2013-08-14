using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context
{
    public static class ConfigurationRegistrarExtensions
    {

        private static readonly Type EntityMappingConfigurationOpenGenericType =
            typeof(EntityTypeConfiguration<>);

        private static readonly Type ComplexMappingConfigurationOpenGenericType =
            typeof(ComplexTypeConfiguration<>);

        private static readonly IEnumerable<MethodInfo> AddMethods = 
            typeof(ConfigurationRegistrar).GetMethods().Where(m => m.Name.Equals("Add")).ToList();

        private static MethodInfo GetAddMethod(this Type mappingConfigurationOpenGenericType)
        {
            return
                AddMethods.First(m => m.GetParameters()[0]
                                          .ParameterType
                                          .GetGenericTypeDefinition()
                                          .IsAssignableFrom(mappingConfigurationOpenGenericType));
        }

        private static readonly IEnumerable<MappingConfiguration> MappingConfigurations = new[]
            {
                new MappingConfiguration(EntityMappingConfigurationOpenGenericType.GetAddMethod(),
                                         EntityMappingConfigurationOpenGenericType),

                new MappingConfiguration(ComplexMappingConfigurationOpenGenericType.GetAddMethod(),
                                         ComplexMappingConfigurationOpenGenericType)
            };


        private static void Add(this ConfigurationRegistrar configurationRegistrar,Type modelConfigurationType, Type modelType, MethodInfo addMethod)
        {
            
            if (modelType != null)
            {
                addMethod
                    .MakeGenericMethod(modelType)
                    .Invoke(configurationRegistrar, new[] { Activator.CreateInstance(modelConfigurationType) });

            }  
        }


        private class MappingConfiguration
        {
            public MethodInfo AddMethod { get; private set; }
            public Type ModelMappingConfigurationOpenGenericType { get; private set; }

            public MappingConfiguration(MethodInfo addMethod, Type modelMappingConfigurationOpenGenericType)
            {
                AddMethod = addMethod;
                ModelMappingConfigurationOpenGenericType = modelMappingConfigurationOpenGenericType;
            }
        }

        public static void AddRange(this ConfigurationRegistrar configurationRegistrar,
                                    IEnumerable<Type> modelMappingConfigurationTypes)
        {
            foreach (var modelMappingConfigurationType in modelMappingConfigurationTypes)
            {
                configurationRegistrar.Add(modelMappingConfigurationType);
            }
        }

        public static void Add(this ConfigurationRegistrar configurationRegistrar, Type modelMappingConfigurationType)
        {
            foreach (var configuration in MappingConfigurations)
            {
                var modelType =
                    modelMappingConfigurationType
                        .GetModelType(configuration.ModelMappingConfigurationOpenGenericType);

                if (modelType != null)
                {
                    configurationRegistrar.Add(modelMappingConfigurationType,
                                               modelType,
                                               configuration.AddMethod);
                }
            }
        }
    }
}