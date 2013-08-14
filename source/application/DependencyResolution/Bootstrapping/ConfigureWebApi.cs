using System.Web.Http;
using Autofac;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Bootstrapping
{
    public class ConfigureWebApi : IStartable
    {

        public void Start()
        {
            Register(GlobalConfiguration.Configuration);
        }

        private static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }

        
    }
}