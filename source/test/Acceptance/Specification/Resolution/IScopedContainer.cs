using System;

namespace Intrigma.DonorSpace.Acceptance.Specification.Resolution
{
    public interface IScopedContainer : IDisposable
    {
        T Resolve<T>();
    }
}