using System.Collections.Generic;
using Intrigma.DonorSpace.Core.ApplicationMessage;

namespace Intrigma.DonorSpace.Core.Interfaces.Commands
{
    public interface IExecutionResult : IEnumerable<KeyValuePair<MessageCategory, MessageGroup>>
    {
        bool IsSuccessFull { get;  }

        void Add(MessageCategory messageCategory, string p, params string[] propertyNames);
    }
}