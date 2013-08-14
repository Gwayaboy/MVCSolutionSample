using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;

namespace Intrigma.DonorSpace.Infrastructure.Web.ActionResults
{
    public class FormActionResult<TForm> : ActionResult
        where TForm : class
    {
        private readonly IFormProcessor _formProcessor;

        public TForm Form { get; private set; }
        public ExecutionResult Result { get; set; }
        public ActionResult Success { get; set; }
        public ActionResult Failure { get; set; }
        public ActionResult ActionThatWasInvoked { get; set; }
        public IList<Action> FailureActions { get; set; }

        public FormActionResult(TForm form, IFormProcessor formProcessor)
        {
            _formProcessor = formProcessor;
            Form = form;
            FailureActions = new List<Action>();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Execute(context.Controller.ViewData.ModelState);
            ActionThatWasInvoked.ExecuteResult(context);

        }

        private bool ModelStateIsValid(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                ActionThatWasInvoked = Failure;
                Result = Result ?? new ExecutionResult();
                Result.MergeWith(modelState);

                return false;
            }
            return true;
        }


        public ExecutionResult Execute(ModelStateDictionary modelState)
        {

            var dictionaryIsNotSyncronisedWithExecutionResult = false;
            if (ModelStateIsValid(modelState))
            {
                Result = _formProcessor.Execute(Form);
                ActionThatWasInvoked = Result.IsSuccessFull ? Success : Failure;
                dictionaryIsNotSyncronisedWithExecutionResult = true;
            }

            if (!Result.IsSuccessFull)
            {
                if (dictionaryIsNotSyncronisedWithExecutionResult) modelState.MergeWith(Result);
                foreach (var action in FailureActions)
                {
                    action();
                }
            }
            return Result;
        }

    }
}