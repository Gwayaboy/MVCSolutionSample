using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population
{
    public class PopulateAllEnumerations : DataContextPopulator
    {
  private static IEnumerable<IPopulate<DataContext>> EnumerationPopulators
        {
            get
            {
                return
                    typeof (DataContext)
                        .GetProperties()
                        .Where(IsEnumerationDbSet)
                        .Select(EnumerationTypeFromDbSet)
                        .Select(CloseGenericEnumerationPopulatorWithEnumerationType)
                        .Select(CreateEnumerationPopulatorInstance);
            }
        }

        private static IPopulate<DataContext> CreateEnumerationPopulatorInstance(Type closedPopulateEnumerationType)
        {
            return (IPopulate<DataContext>) Activator.CreateInstance(closedPopulateEnumerationType);
        }

        private static  Type CloseGenericEnumerationPopulatorWithEnumerationType(Type enumerationType)
        {
            return typeof (PopulateEnumeration<>).MakeGenericType(enumerationType);
        }

        private static Type EnumerationTypeFromDbSet(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetGenericArguments()[0];
        }

        private static bool IsEnumerationDbSet(PropertyInfo property)
        {
            return property.PropertyType.IsGenericType &&
                   property.PropertyType.GetGenericTypeDefinition() == typeof (IDbSet<>) &&
                   property.PropertyType.GetGenericArguments()[0].IsEnumerationConcreteType();
        }

        public override void Populate(DataContext context)
        {
            foreach (var enumerationPopulator in EnumerationPopulators)
            {
                enumerationPopulator.Populate(context);
            }
        }
    }
}