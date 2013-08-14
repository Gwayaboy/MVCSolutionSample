namespace Panzea.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public interface ISystemUnderTest<T>
    {
        T SUT { get; set; }
    }
}