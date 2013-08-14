using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Features.Indexed;
using TypeExtensions = Intrigma.DonorSpace.Infrastructure.Extensions.TypeExtensions;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution
{
    public static class AutofacExtensions
    {
        public static void RegisterDynamicFactoryFor<T>(this ContainerBuilder builder)
        {
            builder.Register<Func<Type, T>>(context =>
                {
                    var handlers = context.Resolve<IIndex<Type, T>>();
                    return t => handlers[t];
                });
        }

        public static Func<T> HttpRequestScopedFactoryFor<T>()
        {
            return () => DependencyResolver.Current.GetService<T>();
        }

        public static Func<Type, object> HttpRequestScopedFactory
        {
            get { return DependencyResolver.Current.GetService; }
        }

        public static IEnumerable<T> ResolveConcreteTypesAs<T>(this IEnumerable<TypeExtensions.ConcreteTypeAndInterface> scanningResults, Func<Type, object> serviceResolver = null)
        {
            var serviceLocator = serviceResolver ?? HttpRequestScopedFactory;
            return 
                scanningResults
                    .Select(result => (T)serviceLocator(result.ConcreteType));
        }
    }
}