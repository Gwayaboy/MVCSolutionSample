using Autofac;
using FluentValidation;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.Factories;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using AssemblyScanner = Intrigma.DonorSpace.Infrastructure.Extensions.AssemblyScanner;
using FluentValidationAssemblyScanner = FluentValidation.AssemblyScanner;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class FluentValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterDynamicFactoryFor<IValidator>();
            RegisterFormValidators(builder);

        }

        private void RegisterFormValidators(ContainerBuilder builder)
        {
            builder.RegisterType<FormValidatorFactory>().As<IFormValidatorFactory>();

            var scanningResults =
                FluentValidationAssemblyScanner.FindValidatorsInAssembly(AssemblyScanner.FromWebAssembly());

            foreach (var scanningResult in scanningResults)
            {
                var formValidatorType = scanningResult.InterfaceType;
                builder.RegisterType(scanningResult.ValidatorType).Keyed<IValidator>(formValidatorType);
            }
        }

       
    }
}