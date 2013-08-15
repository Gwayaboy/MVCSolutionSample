using System;
using Autofac;
using Autofac.Core.Activators.Reflection;
using Intrigma.DonorSpace.Infrastructure;

namespace Intrigma.DonorSpace.Acceptance.Specification.Resolution
{
    public class AutofacLifeTimeScopedContainer : Disposable, IScopedContainer
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacLifeTimeScopedContainer(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public T Resolve<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }
        
        protected override void DisposeCore()
        {
            _lifetimeScope.Dispose();
        }
    }
}