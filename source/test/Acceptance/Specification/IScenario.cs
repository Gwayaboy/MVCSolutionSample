using Intrigma.DonorSpace.Acceptance.Specification.Resolution;

namespace Intrigma.DonorSpace.Acceptance.Specification
{
    public interface IScenario : ISpecification
    {
        int Number { get; }
        
    }
}