using System;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Web;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;

namespace Intrigma.DonorSpace.Acceptance.Specification.ScenarioContext
{
    public class PostControllerScenario<TController, TForm>
        : ControllerScenario<TController, ActionResult, TForm>
        where TController : BaseController
        where TForm : class
    {
        public TForm Form { get; set; }
        private IFormValidatorFactory FormValidatorFactory { get; set; }
        public ExecutionResult ExecutionResult { get; private set; }


        public override void ExecuteAction(Func<TController, ActionResult> func)
        {
            base.ExecuteAction(func);
            var modelState = ValidateForm(Form);
            var result = (FormActionResult<TForm>) ActionResult;

            ExecutionResult = result.Execute(modelState);

            ActionResult = result.ActionThatWasInvoked;
        }

        private ModelStateDictionary ValidateForm(TForm form)
        {
            var validator = FormValidatorFactory.GetValidator<TForm>();
            var dictionnary = new ModelStateDictionary();

            if (validator != null)
            {
                var validationResult = validator.Validate(form);
                foreach (var error in validationResult.Errors)
                {
                    dictionnary.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }


            return dictionnary;
        }
    }
}