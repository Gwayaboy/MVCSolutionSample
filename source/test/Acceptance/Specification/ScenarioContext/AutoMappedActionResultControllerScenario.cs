using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public abstract class AutoMappedActionResultControllerScenario<TController, TActionResult, TModel>
        : ControllerScenario<TController, TActionResult, TModel>
        where TController : BaseController
        where TActionResult : ActionResult, IAutoMapViewModel
    {
        public override void BuildViewModel()
        {
            ViewModel = (TModel)ActionResult.BuildViewModel();
        }

    }
}