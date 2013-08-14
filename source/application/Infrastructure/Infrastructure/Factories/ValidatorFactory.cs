using System;
using Autofac;
using FluentValidation;

namespace Panzea.DonorSpace.Infrastructure.Factories
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer _container;

        public ValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object result;
            _container.TryResolve(validatorType, out result);
          
            return result as IValidator;
        }
    }
}