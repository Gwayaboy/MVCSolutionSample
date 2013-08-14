using System;
using System.Collections.Generic;
using System.Linq;

namespace Panzea.DonorSpace.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public class ConcreteTypeAndInterface
        {
            public ConcreteTypeAndInterface(Type concreteType, Type openGenericInterfaceType)
            {
                ConcreteType = concreteType;
                InterfaceType = openGenericInterfaceType;
            }
            public Type ConcreteType { get; private set; }

            public Type InterfaceType { get; private set; }
        }

        public static bool IsConcreteTypeOf<T>(this Type pluggedType)
        {
            return pluggedType.IsConcreteTypeOf(typeof(T));
        }

        public static bool IsConcreteTypeOf(this Type pluggedType, Type concreteType)
        {
            return pluggedType.IsConcrete() && concreteType.IsAssignableFrom(pluggedType);
        }

        public static bool IsNotConcreteTypeOf<T>(this Type pluggedType)
        {
            return !IsConcreteTypeOf<T>(pluggedType);
        }

        public static bool IsConcrete(this Type type)
        {
            return type != null && !type.IsAbstract && !type.IsInterface;
        }

        public static bool IsNotConcrete(this Type type)
        {
            return !type.IsConcrete();
        }

        public static IEnumerable<Type> Matching(this IEnumerable<Type> types, Func<Type, bool> predicate)
        {
            if (types == null) return Enumerable.Empty<Type>();
            return types.Where(predicate);
        }

        public static IEnumerable<ConcreteTypeAndInterface> GetConcreteTypesImplementingOpenGenericInterfaceType(this IEnumerable<Type> withinTypes, Type openGenericInterfaceType)
        {
            return
                from t in withinTypes
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == openGenericInterfaceType && t.IsConcrete()
                select new ConcreteTypeAndInterface(t, i );
        }
    }

    
}
