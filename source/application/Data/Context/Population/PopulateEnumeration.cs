using System.Data.Entity;
using System.Reflection;
using Intrigma.DonorSpace.Core.Domain.Base;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population
{
    public class PopulateEnumeration<TEnumeration> : DataContextPopulator
        where TEnumeration : Enumeration<TEnumeration>
    {
        private readonly PropertyInfo _enumerationDbSetProperty =
            PropertyExtensions.FindPropertyFor<DataContext>(p => p.PropertyType == typeof (IDbSet<TEnumeration>));
      
        public override void Populate(DataContext context)
        {
            if (_enumerationDbSetProperty == null) return;

            var enumerationDbSet = (IDbSet<TEnumeration>)_enumerationDbSetProperty.GetValue(context, null);

            foreach (var enumeration in Enumeration<TEnumeration>.GetAll())
            {
                enumerationDbSet.Add(enumeration);
            }

            context.SaveChanges();
        }

    }
}