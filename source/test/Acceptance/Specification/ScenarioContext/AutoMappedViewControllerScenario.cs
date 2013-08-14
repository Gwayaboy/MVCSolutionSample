using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class AutoMappedViewControllerScenario<TController, TViewModel>
        : AutoMappedActionResultControllerScenario<TController, AutoMappedViewResult, TViewModel>
        where TController : BaseController
        where TViewModel : class
    { }
}