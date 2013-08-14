using System;
using System.Collections.Generic;

namespace Intrigma.DonorSpace.Core.ApplicationMessage
{
    public class MessageGroup
    {
        public string[] PropertyNames { get; private set; }

        public List<string> Messages { get; private set; }

        public MessageGroup(List<String> messages = null, params string[] propertyNames)
        {
            Messages = messages ?? new List<string>();
            PropertyNames = propertyNames ?? new string[]{} ;
        }


        // <summary>
        /// Clone a message group to a brand new
        /// </summary>
        /// <param name="messageGroup"></param>
        /// <returns></returns>
        public MessageGroup Clone()
        {
            return new MessageGroup(Messages, PropertyNames);
        }


    }
}