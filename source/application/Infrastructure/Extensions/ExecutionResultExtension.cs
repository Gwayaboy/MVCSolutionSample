using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Humanizer;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Commands;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
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