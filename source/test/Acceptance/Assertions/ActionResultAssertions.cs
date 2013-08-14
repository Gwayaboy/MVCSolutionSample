using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentAssertions;
using Intrigma.DonorSpace.Infrastructure.Web.ActionResults;
using MvcContrib.TestHelper;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public class ActionResultAssertions : ActionResultAssertionBase<ActionResult>
    {
        public ActionResultAssertions(ActionResult actionResult) : base(actionResult)
        {
        }

        public void BeRedirectedToUrl(string url)
        {
            VerifyCondition(a => a.AssertHttpRedirect().ToUrl(url));
        }
        
        public void BeRedirectedToAction<TController>(Expression<Action<TController>> action)
            where TController : IController
        {
            VerifyCondition(a => a.AssertActionRedirect().ToAction(action));
        }

        public AndConstraint<FormActionResultAssertion<TForm>> BeFormActionResultOf<TForm>() 
            where TForm : class
        {

            var formActionResult = VerifyCondition(a => a.AssertResultIs<FormActionResult<TForm>>());
            return new AndConstraint<FormActionResultAssertion<TForm>>(new FormActionResultAssertion<TForm>(formActionResult));   
        }

        public AndConstraint<T> BeJsonResultOf<T>() 
            where T : class
        {
            var result = VerifyCondition(a =>
                {
                    var data = a.AssertResultIs<JsonResult>().Data;
                    var dataTypeName = typeof (T).Name;
                    data.ShouldNotBeNull(string.Format("Json data {0} is not expected to be Null",dataTypeName));

                    return
                        data.ShouldBe<T>(
                            string.Format(
                                "Action result is expected Json result with data of type {0} but is actually {1}",
                                dataTypeName, ActionResult.GetType()));
                });

            return new AndConstraint<T>(result);
        }


        public void BeDefaultViewForAction()
        {
            VerifyCondition(a => a.AssertViewRendered());
        }

        
    }
}