using FluentValidation;
using Intrigma.DonorSpace.Web.ViewModels;

namespace Intrigma.DonorSpace.Web.FormValidators
{
    public class AddAdministratorFormValidator : AbstractValidator<AddAdministratorForm>
    {
        public AddAdministratorFormValidator()
        {
            RuleFor(x => x.UserName).EmailAddress();
            RuleFor(x => x.FullName).NotEmpty();

            RuleFor(x => x.Password).MustNotBeEmptyAndHaveBetween3And25Characters();

            RuleFor(x => x.ConfirmedPassword)
                .MustNotBeEmptyAndHaveBetween3And25Characters()
                .Must(BeTheSamePassword)
                .WithMessage("The password must be the same");

        }

        private bool BeTheSamePassword(AddAdministratorForm form, string confirmedPassword)
        {
            return form.Password == confirmedPassword;
        }
    }
}