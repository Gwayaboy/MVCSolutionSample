using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Intrigma.DonorSpace.Infrastructure.Web.CustomValidators
{
    public class SimpleUnitaryClientSideValidator : FluentValidationPropertyValidator
    {
        private readonly string _validationType;

        public SimpleUnitaryClientSideValidator(ModelMetadata metadata,
                                                ControllerContext controllerContext,
                                                PropertyRule rule,
                                                IPropertyValidator validator,
                                                string validationType)
            : base(metadata, controllerContext, rule, validator)
        {
            _validationType = validationType;
        }
        
        //public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        //{
        //    if (!ShouldGenerateClientSideRules())
        //    {
        //        yield break;
        //    }


        //    var errorMessage =
        //        new MessageFormatter()
        //            .AppendPropertyName(Rule.GetDisplayName())
        //            .BuildMessage(Validator.ErrorMessageSource.GetString());

        //    yield return new ModelClientValidationRule
        //        {
        //            ErrorMessage = errorMessage,
        //            ValidationType = _validationType
        //        };
        //}
    }
}