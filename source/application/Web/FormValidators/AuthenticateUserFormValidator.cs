using FluentValidation;
using Intrigma.DonorSpace.Web.ViewModels;

namespace Intrigma.DonorSpace.Web.FormValidators
{
    public class AuthenticateUserFormValidator : AbstractValidator<AuthenticateUserForm>
    {
        public AuthenticateUserFormValidator()
        {
            RuleFor(x => x.CharityId).NotEmpty().WithMessage("a Charity must be specified");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("user name must be specified");
            RuleFor(x => x.Password).NotEmpty().WithMessage("password must be specified");
        }
    }
}