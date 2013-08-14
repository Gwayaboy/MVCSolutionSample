using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Bootstrapping
{
    public class ConfigureRoutes : IStartable
    {
        public void Start()
        {
            RouteTable.Routes.Clear();
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            Guard.That(() => routes).IsNotNull();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            // Default MVC Route
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = 1 } // Parameter defaults
                );
        }
    }
}