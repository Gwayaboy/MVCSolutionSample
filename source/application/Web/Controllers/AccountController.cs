using System;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;
using Intrigma.DonorSpace.Web.ViewModels;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace Intrigma.DonorSpace.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(int id, string returnUrl = "")
        {
            return View(new AuthenticateUserForm { ReturnUrl = returnUrl, CharityId = id });
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AuthenticateUserForm authenticateUserForm)
        {
            return
                HandleForm(authenticateUserForm)
                    .WithSuccessResult(RedirectToLocal(authenticateUserForm.ReturnUrl));
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login",new { id = 1});
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AuthenticateUserForm userForm)
        {
            
            throw new NotImplementedException();
            
        }

        //
        // POST: /Account/Disassociate

    

       

        

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

       
       
    }
}
