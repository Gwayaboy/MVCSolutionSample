using System.Web.Mvc;
using Panzea.DonorSpace.Infrastructure.Interfaces;

namespace Panzea.DonorSpace.Infrastructure.Web
{
    public class BaseController : Controller
    {
        public IFormProcessor FormProcessor { get; internal set; }

        protected FormActionResult<TForm> HandleForm<TForm>(TForm form)
           where TForm : class
        {
            return new FormActionResult<TForm>(form, FormProcessor)
                .WithFailureResult(View(form));
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Home");
        }
    }
}