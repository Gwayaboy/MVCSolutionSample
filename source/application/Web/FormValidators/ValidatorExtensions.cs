using FluentValidation;
using Intrigma.DonorSpace.Web.FormValidators.CustomValidators;

namespace Intrigma.DonorSpace.Web.FormValidators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustNotBeEmptyAndHaveBetween3And25Characters<T>
            (this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return
                ruleBuilder.SetValidator(new MustNotBeEmptyAndHaveBetween3And25CharactersValidator());
        }
    }
}