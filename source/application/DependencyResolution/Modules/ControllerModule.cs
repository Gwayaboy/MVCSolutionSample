using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Autofac.Integration.Mvc;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Web;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class ControllerModule : Module
    {
        private static Action<IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>>
          _lifeStyleProvider = x => x.InstancePerHttpRequest();

        private static UrlHelper _urlHelper;


        public static void WithUrlHelper(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public static void WithLifeStyle(Action<IRegistrationBuilder<Object, ScanningActivatorData, DynamicRegistrationStyle>> lifeStyleProvider)
        {
            _lifeStyleProvider = lifeStyleProvider;
        }

        protected override void Load(ContainerBuilder builder)
        {
            
            builder
                .RegisterControllers(AssemblyScanner.FromWebAssembly())
                .PropertiesAutowired()
                .ConfigureWith(_lifeStyleProvider)
                .AsSelf()
                .OnActivating(e =>
                    {
                        var controller = e.Instance as BaseController;
                        if (controller == null) return;

                        controller.FormProcessor = e.Context.Resolve<IFormProcessor>();
                        controller.ModelMapper = e.Context.Resolve<IModelMapper>();
                        if (controller.Url == null) controller.Url = _urlHelper;

                    });
        }
    }
}