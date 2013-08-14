using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class ViewControllerScenario<TController, TModel>
        : ControllerScenario<TController, ViewResult, TModel>
        where TController : BaseController
        where TModel : class
    { }
}