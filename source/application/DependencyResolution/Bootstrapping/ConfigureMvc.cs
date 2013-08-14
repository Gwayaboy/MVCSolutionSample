using System.Web.Mvc;
using Autofac;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Bootstrapping
{
    public class ConfigureMvc : IStartable
    {
        public void Start()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
        }

        private void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Guard.That(() => filters).IsNotNull();

            filters.Add(new HandleErrorAttribute());
            
        }
    }
}