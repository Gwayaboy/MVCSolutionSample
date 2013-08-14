using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Web.ViewModels;

namespace Intrigma.DonorSpace.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ICharityQuery _charityQuery;

        public HomeController(ICharityQuery charityQuery )
        {
            _charityQuery = charityQuery;
        }

        public ActionResult Index(int id)
        {
            var charity = _charityQuery.Get(id);

            return AutoMappedViewWith<AuthenticatedUserViewModel>(charity);
        }

        
    }
}
