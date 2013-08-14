using System.Collections.Generic;
using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;
using Intrigma.DonorSpace.Web.ViewModels;
using MvcContrib;

namespace Intrigma.DonorSpace.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : BaseController
    {
        private readonly ICharityQuery _charityQuery;
        private readonly IAdministratorQuery _administratorQuery;

        public AdministrationController(ICharityQuery charityQuery,
                                        IAdministratorQuery administratorQuery)
        {
            _charityQuery = charityQuery;
            _administratorQuery = administratorQuery;
        }

        public AutoMappedViewResult Index(int id)
        {
            var charity = _charityQuery.Get(id);
            
           return AutoMappedViewWith<CharityViewModel>(charity);
        }

        public AutoMappedViewResult Administrators(int id)
        {
            var charity = _charityQuery.Get(id);

            return AutoMappedViewWith<IEnumerable<AdministratorDetailViewModel>>(charity);
        }

       

        public AutoMappedViewResult Donors()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult UpdateAuthenticationDetails(CharityAuthenticationDetailsForm form)
        {
            return HandleFormAndReturnExecutionResultAsJsonResult(form);
        }

        [HttpGet]
        public ViewResult AddAdministrator(int id)
        {
            return View(new AddAdministratorForm {CharityId = id});
        }

        [HttpPost]
        public ActionResult AddAdministrator(AddAdministratorForm form)
        {
            return
                HandleForm(form)
                    .WithSuccessResult(this.RedirectToAction(x => x.Administrators(form.CharityId)));
        }


        public ActionResult DeleteAdministrator(DeleteAdministratorForm form)
        {
            return HandleFormAndReturnExecutionResultAsJsonResult(form);
        }

        [HttpGet]
        public ViewResult Edit(int id,int charityId)
        {
            var administrator = ModelMapper.MapViewModel<EditAdministratorForm>(_administratorQuery.Get(id));
            administrator.CharityId = charityId;
            
            return View(administrator);
        }

        [HttpPost]
        public ActionResult Edit(EditAdministratorForm form)
        {
            return
                HandleForm(form)
                    .WithSuccessResult(this.RedirectToAction(x => x.Administrators(form.CharityId)));
        }
    }
}