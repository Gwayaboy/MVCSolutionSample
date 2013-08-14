using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Panzea.DonorSpace.Core.Domain.Base;
using Panzea.DonorSpace.Infrastructure.Helper;

namespace Panzea.DonorSpace.Infrastructure.Extensions
{
    public  static class PropertyExtensions
    {
        public  static  PropertyInfo FindPropertyMatching<T>(this T instance, Func<PropertyInfo, bool> propertyMatcher)
            where T : class
        {
            PropertyInfo result = null;
            if (instance != null)
            {
                result = instance.GetType().GetProperties().SingleOrDefault(propertyMatcher);
            }

            return result;
        }

        public static PropertyInfo GetPropertyFromLambda(this LambdaExpression propertySelector)
        {
            var propInfo = new ExpressionPropertyVisitor().GetPropertiesFrom(propertySelector).FirstOrDefault();

            if (propInfo == null)
            {
                throw new InvalidOperationException
                    (String.Format("Expression '{0}' refers to a field, not a property.",
                                   propertySelector));
            }

            return propInfo;
        }

        public static PropertyInfo GetPropertyFromLambda<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propertySelector)
            where T : Entity
        {
            if ((obj == null) || propertySelector == null)
            {
                return null;
            }

            return propertySelector.GetPropertyFromLambda();
        }

        public static void SetValue<TDomain, TProperty>(this TDomain domainObject, Expression<Func<TDomain, TProperty>> domainPropertySelector, TProperty value)
            where TDomain : Entity
        {
            if (domainObject != null)
            {
                var property = domainObject.GetPropertyFromLambda(domainPropertySelector);

                property.SetValue(domainObject, value, null);
            }

        }

        public static void SetId<TDomain>(this TDomain domainObject, int newValue)
            where TDomain : Entity
        {
            SetValue(domainObject, x => x.Id, newValue);
        }
    }
}
