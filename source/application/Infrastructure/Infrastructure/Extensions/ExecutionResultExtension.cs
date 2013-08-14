using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Humanizer;
using Panzea.DonorSpace.Core.ApplicationMessage;
using Panzea.DonorSpace.Core.Interfaces.Commands;

namespace Panzea.DonorSpace.Infrastructure.Extensions
{
    /// <summary>
    /// Extension utilities on application message
    /// </summary>
    public static class ExecutionResultExtension
    {
        public static IExecutionResult Merge(this IExecutionResult executionResult, IEnumerable<ValidationFailure> validationFailures, MessageCategory withMessageCategory = MessageCategory.BrokenBusinessRule)
        {
            if (executionResult != null)
            {

                foreach (var validationFailure in validationFailures)
                {
                    executionResult.Add(MessageCategory.BrokenBusinessRule, validationFailure.ErrorMessage,
                                        validationFailure.PropertyName);
                }
              
            }
            return executionResult;
        }

        public static String ToTitle(this MessageCategory messageCategory)
        {
            return messageCategory.Humanize(LetterCasing.Title);
        }
        
        
    }
}