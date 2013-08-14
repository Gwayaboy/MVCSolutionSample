using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class AutoMappedJSonControllerScenario<TController, TModel>
        : AutoMappedActionResultControllerScenario<TController, AutoMappedJsonResult, TModel>
        where TController : BaseController
    { }
}