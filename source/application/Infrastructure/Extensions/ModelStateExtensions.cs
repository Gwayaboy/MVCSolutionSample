using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
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

        public static void MergeWith(this ExecutionResult executionResult, ModelStateDictionary modelStateDictionary)
        {
            if (executionResult == null || modelStateDictionary == null) return;
            foreach (var item in modelStateDictionary)
            {
                var messageGroup = new MessageGroup(item.Value.Errors.Select(e => e.ErrorMessage).ToList(), item.Key);
                executionResult.Add(MessageCategory.BrokenBusinessRule, messageGroup);
            }
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
                AddErrorMessage(modelStateDictionary, messageGroup);

                foreach (var propertyName in messageGroup.PropertyNames)
                {
                    modelStateDictionary.AddModelError(propertyName ?? string.Empty, " ");
                }
            }
        }

        private static void AddErrorMessage(ModelStateDictionary modelStateDictionary, MessageGroup messageGroup)
        {
            foreach (var errorMessage in messageGroup.Messages)
            {
                modelStateDictionary.AddModelError(string.Empty, errorMessage);
            }
        }
    }
}