using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Panzea.DonorSpace.Infrastructure.ApplicationMessage;

namespace Panzea.DonorSpace.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {

        public static List<string> GetAllErrorMessages(this ModelStateDictionary modelState)
        {
            return
                modelState
                    .Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
        }

        public static void MergeWith(this ModelStateDictionary modelStateDictionary, ExecutionResult executionResult)
        {
            if (executionResult == null) return;

            var messageGroups =
                executionResult
                    .Errors
                    .Select(e => e.Value);


            foreach (var messageGroup in messageGroups)
            {
                foreach (var propertyName in  messageGroup.PropertyNames)
                {
                    foreach (var errorMessage in messageGroup.Messages)
                    {
                        modelStateDictionary.AddModelError(propertyName,errorMessage);   
                    }
                }
            }
        }

    }
}