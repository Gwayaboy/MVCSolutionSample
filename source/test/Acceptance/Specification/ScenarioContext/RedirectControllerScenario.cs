using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class RedirectControllerScenario<TController, TForm> : ControllerScenario<TController, RedirectToRouteResult, TForm>
        where TController : BaseController
    { }
}