using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public interface IControllerScenario<TController> 
        where TController:BaseController
    {
        TController Controller { get; set; }
    }
}