using System;
using FluentValidation;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Factories
{
    public class FormValidatorFactory : ValidatorFactoryBase,IFormValidatorFactory
    {
        private readonly Func<Type, IValidator> _formValidatorProvider;

        public FormValidatorFactory(Func<Type,IValidator> formValidatorProvider )
        {
            _formValidatorProvider = formValidatorProvider;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            try
            {
                return _formValidatorProvider.Invoke(validatorType);
            }
            catch
            {
                return null;
            }
        }
    }
}
