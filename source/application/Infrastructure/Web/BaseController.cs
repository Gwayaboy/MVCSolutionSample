using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Web
{
    public abstract class BaseController : Controller
    {
        public IFormProcessor FormProcessor { get; set; }
        public IModelMapper ModelMapper { get; set; }

        protected FormActionResult<TForm> HandleForm<TForm>(TForm form)
            where TForm : class
        {
            return new FormActionResult<TForm>(form, FormProcessor)
                .WithFailureResult(View(form));
        }


        protected FormActionResult<TForm> HandleFormAndReturnExecutionResultAsJsonResult<TForm>(TForm form)
            where TForm : class
        {
            var formActionResult = new FormActionResult<TForm>(form, FormProcessor);

            return
                formActionResult
                    .WithFailureResultAndActionCallBack<TForm, JsonResult>(a => a.Data = formActionResult.Result)
                    .WithSuccessResult(Json(new ExecutionResult()));
        }



        protected AutoMappedViewResult AutoMappedViewWith<TModel>(object entity)
            where TModel : class
        {
            Guard.That(entity).IsNotNull();

            return new AutoMappedViewResult(ModelMapper, entity, typeof (TModel))
                {
                    ViewData = ViewData,
                    TempData = TempData
                };
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}