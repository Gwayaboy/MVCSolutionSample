using Autofac;
using Intrigma.DonorSpace.Infrastructure;

namespace Intrigma.DonorSpace.Acceptance.Specification.Resolution
{
    public class AutofacLifeTimeScope : Disposable, IScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacLifeTimeScope(ILifetimeScope lifetimeScope)
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