using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Commands
{
    public class CommandProcessor : ICommandProcessor
    {


        private readonly Func<Type, ICommandHandler> _commandHandlerFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IExecutionResult _executionResult;
        
        public CommandProcessor(Func<Type,ICommandHandler> commandHandlerFactory, IUnitOfWork unitOfWork)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _unitOfWork = unitOfWork;
        }

        public IExecutionResult Execute<T>(T command) where T : class, ICommand
        {
            Guard.That(() => command).IsNotNull();
            
            _executionResult = new ExecutionResult(command);

            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    HandlerFor(command).Handle();

                    transaction.Commit();
                }
                
            }
            catch (ValidationException validationException)
            {
                OnError(validationException.Errors);
            }
            catch (BusinessRuleException businessRuleException)
            {
                OnError(businessRuleException);
            }
            catch (Exception fatalException)
            {
                OnError(fatalException);
            }
            
            return _executionResult;
        }

        private void OnError(IEnumerable<ValidationFailure> errors)
        {
            _executionResult.Merge(errors);
        }

        private void OnError(BusinessRuleException businessRuleException)
        {
            _executionResult.Add(MessageCategory.BrokenBusinessRule, businessRuleException.Message, businessRuleException.PropertyNames);
        }

        private void OnError(Exception fatalException)
        {
            _executionResult.Add(MessageCategory.FatalException, fatalException.Message);
        }
        
        private ICommandHandler<T> HandlerFor<T>(T command) where T : class, ICommand
        {
            var commandType = typeof (T);
            try
            {
                var handler = (ICommandHandler<T>)_commandHandlerFactory.Invoke(commandType);
                handler.Command = command;
                handler.UnitOfWork = _unitOfWork;
                return handler;
            }
            catch 
            {
                throw new CommandHandlerNotFoundException(commandType);
            }
        }
    }
}
