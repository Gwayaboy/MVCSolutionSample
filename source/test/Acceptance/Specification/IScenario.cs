using System;
using Intrigma.DonorSpace.Acceptance.Helper;
using Intrigma.DonorSpace.Acceptance.Specification.Resolution;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public interface IScenario : ISpecification
    {
        int Number { get; }
        IDatabase Database { get; }
    }
}