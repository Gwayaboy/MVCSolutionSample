using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Humanizer;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public class ExecutionResultAssertions
    {
        public ExecutionResult ExecutionResult { get; private set; }

        public ExecutionResultAssertions(ExecutionResult executionResult)
        {
            ExecutionResult = executionResult;
        }

        public void BeSuccessfull()
        {
            Execute.Verification
                   .ForCondition(ExecutionResult.IsSuccessFull)
                   .FailWith(ExecutionResult.ToString());
        }


        public AndConstraint<ExecutionResultAssertions> BeUnsuccessfull()
        {

            Execute.Verification
                   .ForCondition(!ExecutionResult.IsSuccessFull)
                   .FailWith("Execution result expected to be unsucessfull but found to be successfull");

            return new AndConstraint<ExecutionResultAssertions>(this);
        }

        public AndConstraint<ExecutionResultAssertions> HaveErrorMessage(string errorMessage)
        {
            Execute.Verification
                   .ForCondition(ExecutionResult.AllErrorMessages.Contains(errorMessage))
                   .FailWith("Execution result expected to have error message {0} but was actually {1}", errorMessage,
                             ExecutionResult);

            return new AndConstraint<ExecutionResultAssertions>(this);
        }

        public AndConstraint<ExecutionResultAssertions> HaveErrorMessageOfType(MessageCategory messageCategory)
        {
            Execute.Verification
                   .ForCondition(ExecutionResult.Errors.Any(e => e.Key == messageCategory))
                   .FailWith("Execution result expected to have an error message of type {0}  but actually found {1}",
                             messageCategory.Humanize(LetterCasing.Sentence),
                             AllErrorMessageCategories);

            return new AndConstraint<ExecutionResultAssertions>(this);
        }
        private string AllErrorMessageCategories
        {
            get
            {
                var messageCategories =
                    ExecutionResult
                        .Errors.
                         Select(e => e.Key.Humanize(LetterCasing.Sentence))
                        .ToArray();

                var sb = new StringBuilder();
                for (var i = 0; i < messageCategories.Count() - 1; i++)
                {
                    sb.Append(messageCategories[i] + ",");
                }

                if (messageCategories.Count() > 1)
                {
                    sb.Append(" and ");
                }
                sb.Append(messageCategories.Last());

                return sb.ToString();
            }
        }

        public AndConstraint<ExecutionResultAssertions> HaveNoErrorMessage()
        {
            Execute.Verification
                .ForCondition(!ExecutionResult.AllErrorMessages.Any())
                .FailWith("Execution result expected to have no error message but found {0}", ExecutionResult);
            
            return new AndConstraint<ExecutionResultAssertions>(this);
        }
    }

}