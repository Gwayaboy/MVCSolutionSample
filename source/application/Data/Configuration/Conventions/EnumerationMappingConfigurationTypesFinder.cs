using System;
using System.Collections.Generic;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions
{
    public class EnumerationMappingConfigurationTypesFinder : MappingConfigurationTypesFinder
    {
        public EnumerationMappingConfigurationTypesFinder(IEnumerable<Type> definedMappingConfigurationType) : 
            base(definedMappingConfigurationType)
        {
        }

        public override IEnumerable<Type> MappingConfigurationTypes
        {
            get
            {
                return
                    GetAutoConfigurationMappings(t => t.IsEnumerationConcreteType(),
                                                 enumerationType => typeof (EnumerationConfiguration<>)
                                                                        .MakeGenericType(enumerationType));
            }
        }
    }
}