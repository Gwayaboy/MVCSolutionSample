using Autofac;
using FluentValidation.Mvc;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Bootstrapping
{
    public class ConfigureModelValidatorProvider : IStartable
    {
        private readonly IFormValidatorFactory _formValidatorFactory;

        public ConfigureModelValidatorProvider(IFormValidatorFactory formValidatorFactory)
        {
            _formValidatorFactory = formValidatorFactory;
        }

        public void Start()
        {
            FluentValidationModelValidatorProvider
                .Configure(c => c.ValidatorFactory = _formValidatorFactory);

        }
    }
}