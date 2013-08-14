using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Core.Interfaces.Data;

namespace Intrigma.DonorSpace.Core.Commands
{
    
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
       
        public abstract void Handle();
        public IUnitOfWork UnitOfWork { get; set; }
        
        public TCommand Command { get; set; }


        protected BusinessRuleException BusinessException(string message, Boolean forAllProperties = false)
        {
            var propertyNames = new string[]{};

            if (forAllProperties)
            {
                propertyNames = typeof(TCommand).GetProperties().Select(p => p.Name).ToArray();
            }

            return new BusinessRuleException(message, propertyNames);
        }

        protected void Commit(Action action)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                action();
                transaction.Commit();
            }
        }

 
        protected BusinessRuleException BusinessExceptionFor<TProperty>(Expression<Func<TCommand, TProperty>> commandPropertySelector, string withMessage)
        {
            var propertyName = string.Empty;

            if (commandPropertySelector != null)
            {

                var member = commandPropertySelector.Body as MemberExpression;
                if (member != null)
                {
                    var propInfo = member.Member as PropertyInfo;
                    if (propInfo != null)
                    {
                        propertyName = propInfo.Name;
                    }

                }
            }

            return new BusinessRuleException(withMessage, propertyName);
        }
    }

}