using FluentValidation;
using Intrigma.DonorSpace.Web.ViewModels;

namespace Intrigma.DonorSpace.Web.FormValidators
{
    public class EditAdministratorFormValidator : AbstractValidator<EditAdministratorForm>
    {
        public EditAdministratorFormValidator()
        {
            RuleFor(x => x.CurrentPassword).MustNotBeEmptyAndHaveBetween3And25Characters();
            
            RuleFor(x => x.NewPassword).MustNotBeEmptyAndHaveBetween3And25Characters();

            RuleFor(x => x.NewPassword)
                .Must(BeDifferentFromCurrentPassword)
                .WithMessage("The New password must be different from the current one");
            
            RuleFor(x => x.CharityId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }

        private bool BeDifferentFromCurrentPassword(EditAdministratorForm form, string newPassword)
        {
            return form.CurrentPassword != newPassword;
        }
    }
}