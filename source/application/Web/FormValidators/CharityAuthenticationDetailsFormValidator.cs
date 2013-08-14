using FluentValidation;
using Intrigma.DonorSpace.Web.ViewModels;

namespace Intrigma.DonorSpace.Web.FormValidators
{
    public class CharityAuthenticationDetailsFormValidator : AbstractValidator<CharityAuthenticationDetailsForm>
    {
        public CharityAuthenticationDetailsFormValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("No charity has been specified");
            RuleFor(c => c.WebServiceUrl).NotEmpty().WithMessage("Please provide a web service url");
        }
    }
}