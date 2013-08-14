using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions
{
    public class DefaultMappingConfigurationTypesFinder : MappingConfigurationTypesFinder
    {

        private static readonly Assembly ThisAssembly = typeof (DefaultMappingConfigurationTypesFinder).Assembly;

        public DefaultMappingConfigurationTypesFinder()
            : base(Enumerable.Empty<Type>())
        { }

        public override IEnumerable<Type> MappingConfigurationTypes
        {
            get
            {
                return AssemblyScanner.GetExportedTypesFrom(ThisAssembly).Where(IsMappingConfigurationType);
            }
        }

        private static bool IsMappingConfigurationType(Type candidateType)
        {
            return
                candidateType.IsClosedTypeOf(typeof(EntityTypeConfiguration<>)) ||
                candidateType.IsClosedTypeOf(typeof(ComplexTypeConfiguration<>));

        }
    }
}