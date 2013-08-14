using System;

namespace Intrigma.DonorSpace.Acceptance.Specification.Resolution
{
    public interface IScope : IDisposable
    {
        T Resolve<T>();
    }
}