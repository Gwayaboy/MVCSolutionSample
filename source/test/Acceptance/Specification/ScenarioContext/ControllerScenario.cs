using System;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class ControllerScenario<TController, TActionResult, TModel> : IControllerScenario<TController>
        where TController : BaseController
        where TActionResult : ActionResult
    {
        public TController Controller { get; set; }
        public TActionResult ActionResult { get; set; }
        public Exception Exception { get; set; }
        public TModel ViewModel { get; set; }

        public virtual void ExecuteAction(Func<TController, TActionResult> func)
        {
            try
            {
                ActionResult = func(Controller);
                BuildViewModel();
            }
            catch (Exception ex)
            {
                Exception = ex;
                if (Exception is NotImplementedException) throw ex;
            }
        }

        public virtual void BuildViewModel()
        {
            ViewModel = (TModel) Controller.ViewData.Model;
        }
    }

    
}