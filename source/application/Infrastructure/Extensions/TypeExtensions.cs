using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
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
        
        public static bool IsConcreteTypeOf<T>(this Type candidateType)
        {
            return candidateType.IsConcreteTypeOf(typeof(T));
        }

        public static bool IsSubClassOf<T>(this Type derivedType)
        {
            return derivedType.IsSubclassOf(typeof(T));
        }

        public static bool IsConcreteTypeOf(this Type candidateType, Type concreteType)
        {
            return candidateType.IsConcrete() && concreteType.IsAssignableFrom(candidateType);
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

        public static bool IsEnumerationConcreteType(this Type candidateType)
        {
            return
                candidateType.BaseType != null &&
                candidateType.BaseType.IsGenericType &&
                candidateType.BaseType.GetGenericTypeDefinition() == typeof(Enumeration<>) &&
                candidateType.IsConcreteTypeOf(typeof(Enumeration<>).MakeGenericType(candidateType));
        }

        /// <summary>Determines whether the candidate type supports any base or 
        /// interface that closes the provided generic type.</summary>
        /// <param name="canditateType"></param>
        /// <param name="openGeneric"></param>
        /// <returns></returns>
        internal static bool IsClosedTypeOf(this Type canditateType, Type openGeneric)
        {
            if (canditateType == null) throw new ArgumentNullException("canditateType");
            if (openGeneric == null) throw new ArgumentNullException("openGeneric");

            if (!(openGeneric.IsGenericTypeDefinition || openGeneric.ContainsGenericParameters))
                throw new ArgumentException(string.Format("The type '{0}' is not an open generic class or interface type.", openGeneric.FullName));

            return canditateType.GetTypesThatClose(openGeneric).Any();
        }

        /// <summary>Returns the first concrete interface supported by the candidate type that
        /// closes the provided open generic service type.</summary>
        /// <param name="this">The type that is being checked for the interface.</param>
        /// <param name="openGeneric">The open generic type to locate.</param>
        /// <returns>The type of the interface.</returns>
        public static IEnumerable<Type> GetTypesThatClose(this Type @this, Type openGeneric)
        {
            return FindAssignableTypesThatClose(@this, openGeneric);
        }

        /// <summary>
        /// Looks for an interface on the candidate type that closes the provided open generic interface type.
        /// </summary>
        /// <param name="candidateType">The type that is being checked for the interface.</param>
        /// <param name="openGenericServiceType">The open generic service type to locate.</param>
        /// <returns>True if a closed implementation was found; otherwise false.</returns>
        static IEnumerable<Type> FindAssignableTypesThatClose(Type candidateType, Type openGenericServiceType)
        {
            return TypesAssignableFrom(candidateType)
                .Where(t => IsGenericTypeDefinedBy(t, openGenericServiceType));
        }

        static IEnumerable<Type> TypesAssignableFrom(Type candidateType)
        {
            return candidateType.GetInterfaces().Concat(
                Traverse.Across(candidateType, t => t.BaseType));
        }

        public static bool IsGenericTypeDefinedBy(this Type @this, Type openGeneric)
        {
            if (@this == null) throw new ArgumentNullException("this");
            if (openGeneric == null) throw new ArgumentNullException("openGeneric");

            return !@this.ContainsGenericParameters && @this.IsGenericType && @this.GetGenericTypeDefinition() == openGeneric;
        }


        public static IEnumerable<Type> Matching(this IEnumerable<Type> types, Func<Type, bool> predicate)
        {
            if (types == null) return Enumerable.Empty<Type>();
            return types.Where(predicate);
        }

        public static IEnumerable<ConcreteTypeAndInterface> GetClosedTypesOf(this IEnumerable<Type> withinTypes,
                                                                             Type openGenericInterfaceType)
        {
            return
                withinTypes
                    .GetConcreteTypesImplementingOpenGenericInterfaceType(openGenericInterfaceType)
                    .Where(t => t.ConcreteType.IsClosedTypeOf(openGenericInterfaceType));
        } 

        public static IEnumerable<ConcreteTypeAndInterface> GetConcreteTypesImplementingOpenGenericInterfaceType(this IEnumerable<Type> withinTypes, Type openGenericInterfaceType)
        {
            return
                from t in withinTypes
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == openGenericInterfaceType && t.IsConcrete()
                select new ConcreteTypeAndInterface(t, i );
        }

        public static IEnumerable<ConcreteTypeAndInterface> GetConcreteTypesImplementing<T>(this IEnumerable<Type> withinTypes)
        {
            return
                from t in withinTypes
                from i in t.GetInterfaces()
                where  t.IsConcreteTypeOf<T>()
                select new ConcreteTypeAndInterface(t, i);
        }

        public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            Type destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }

        public static bool EntityHasIdOfType<TId>(this MemberInfo memberInfo, string withPropertyNamed = "Id")
        {
            return memberInfo.Name == withPropertyNamed && memberInfo.DeclaringType.CanBeCastTo<EntityWithTypedId<TId>>();
        }

        public static Type GetModelType(this Type candidateType, Type openGenericType)
        {

            return 
                Traverse
                    .Across(candidateType, t => t.BaseType)
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == openGenericType)
                    .Select(t => t.GetGenericArguments().First())
                    .FirstOrDefault();
            
        }
    }

    
}
