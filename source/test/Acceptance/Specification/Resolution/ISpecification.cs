using System;

namespace Intrigma.DonorSpace.Acceptance.Specification.Resolution
{
    public interface ISpecification
    {
        Type Story { get; }
        string Title { get; }
    }
}