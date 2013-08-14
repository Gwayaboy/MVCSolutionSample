using System.Web;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution;

namespace Intrigma.DonorSpace.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ApplicationConfigurator.Bootstrap();
        }
    }
}