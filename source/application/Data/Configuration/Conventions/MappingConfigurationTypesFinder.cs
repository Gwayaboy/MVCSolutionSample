using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions
{
    public abstract class MappingConfigurationTypesFinder 
    {
        protected IEnumerable<Type> DefinedMappingConfigurationType { get; private set; }
        public abstract IEnumerable<Type> MappingConfigurationTypes { get; }

        protected virtual Assembly DomainAssembly {get { return typeof (Entity).Assembly; } }

        protected MappingConfigurationTypesFinder(IEnumerable<Type> definedMappingConfigurationType)
        {
            DefinedMappingConfigurationType = definedMappingConfigurationType;
        }

        protected IEnumerable<Type> GetAutoConfigurationMappings(Func<Type, bool> predicate,
                                                                 Func<Type, Type> typeSelector)
        {
            var configuredEntityTypes =
                DefinedMappingConfigurationType
                    .Select(t => t.GetModelType(typeof (EntityTypeConfiguration<>)))
                    .Union(DefinedMappingConfigurationType.Select(t => t.GetModelType(typeof(ComplexTypeConfiguration<>))))
                    .ToArray();
    
            return
                AssemblyScanner
                    .GetExportedTypesFrom(DomainAssembly)
                    .Where(predicate)
                    .Where(t => !configuredEntityTypes.Contains(t))
                    .Select(typeSelector);
        }

    }
}