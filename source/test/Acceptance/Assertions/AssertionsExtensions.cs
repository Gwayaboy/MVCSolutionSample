using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public static class AssertionsExtensions
    {
        public static ActionResultAssertions Should(this ActionResult actionResult)
        {
            return new ActionResultAssertions(actionResult);

        }

        public static ExecutionResultAssertions Should(this ExecutionResult executionResult)
        {
            return new ExecutionResultAssertions(executionResult);
        }

        public static ExecutionResultAssertions Should(this IExecutionResult executionResult)
        {
            return ((ExecutionResult) executionResult).Should();
        }
    }
}