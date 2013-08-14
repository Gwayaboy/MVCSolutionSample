using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;
using MvcContrib.TestHelper;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public class FormActionResultAssertion<TForm> : ActionResultAssertionBase<FormActionResult<TForm>> 
        where TForm : class 
    {
        public FormActionResultAssertion(FormActionResult<TForm> actionResult) : base(actionResult)
        {
        }

        public void BeOnSuccessRedirectedToUrl(string url)
        {
            VerifyCondition(a => a.Success.AssertHttpRedirect().ToUrl(url));
        }

        public void BeOnSuccessRedirectedToAction<TController>(Expression<Action<TController>> action)
            where TController : IController
        {
            VerifyCondition(a => a.Success.AssertActionRedirect().ToAction(action));
        }
    }
}