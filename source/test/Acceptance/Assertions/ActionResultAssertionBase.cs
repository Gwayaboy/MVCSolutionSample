using System;
using System.Web.Mvc;
using FluentAssertions.Execution;
using MvcContrib.TestHelper;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public  abstract class ActionResultAssertionBase<TActionResult>
        where TActionResult : ActionResult
    {
        public TActionResult ActionResult { get; private set; }

        protected ActionResultAssertionBase(TActionResult actionResult)
        {
            ActionResult = actionResult;
        }

        protected TResult VerifyCondition<TResult>(Func<TActionResult, TResult> action)
        {
            string errorMessage;
            TResult result;
            var condition = ConditionFor(action, out errorMessage, out result);

            Execute.Verification.ForCondition(condition).FailWith(errorMessage);

            return result;
        }

        private bool ConditionFor<TResult>(Func<TActionResult, TResult> assertAndReturn, out string errorMessage, out TResult result)
        {

            var condition = true;
            result = default(TResult);
            errorMessage = string.Empty;
            try
            {
                result = assertAndReturn(ActionResult);

            }
            catch (ActionResultAssertionException ex)
            {

                errorMessage = ex.Message;
                condition = false;
            }

            return condition;


        }


    }
}