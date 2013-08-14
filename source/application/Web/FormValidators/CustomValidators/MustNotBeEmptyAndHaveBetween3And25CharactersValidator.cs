using FluentValidation.Validators;

namespace Intrigma.DonorSpace.Web.FormValidators.CustomValidators
{
    public class MustNotBeEmptyAndHaveBetween3And25CharactersValidator : PropertyValidator
    {

        public MustNotBeEmptyAndHaveBetween3And25CharactersValidator()
            : base("{PropertyName} should not be empty and have a minimum of 3 characters and maximum of 25")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var password = context.PropertyValue as string;

            return
                !string.IsNullOrWhiteSpace(password) &&
                password.Length >= 3 &&
                password.Length <= 25;

        }

        
    }



    
}