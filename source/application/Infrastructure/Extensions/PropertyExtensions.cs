using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Intrigma.DonorSpace.Infrastructure.Helper;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
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

        public static PropertyInfo FindPropertyFor<T>(Func<PropertyInfo, bool> propertyMatcher)
           where T : class
        {
            return typeof(T).GetProperties().SingleOrDefault(propertyMatcher);
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
            where T : class
        {
            if ((obj == null) || propertySelector == null)
            {
                return null;
            }

            return propertySelector.GetPropertyFromLambda();
        }

        
    }
}
