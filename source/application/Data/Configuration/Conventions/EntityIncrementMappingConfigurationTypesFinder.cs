using System;
using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration.Conventions
{
    public class EntityIncrementMappingConfigurationTypesFinder : MappingConfigurationTypesFinder
    {
        public EntityIncrementMappingConfigurationTypesFinder(IEnumerable<Type> definedMappingConfigurationType)
            : base(definedMappingConfigurationType)
        {
        }

        public override IEnumerable<Type> MappingConfigurationTypes
        {
            get
            {
                return
                    GetAutoConfigurationMappings(type => type.IsConcreteTypeOf(typeof (Entity)),
                                                 entityType => typeof(EntityIdIncrementConfiguration<>)
                                                                   .MakeGenericType(entityType));
            }
        }
    }
}