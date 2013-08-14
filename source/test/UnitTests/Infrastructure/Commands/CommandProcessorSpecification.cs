using System;
using Intrigma.DonorSpace.Acceptance.Fakes;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Core.Interfaces.Data;
using Intrigma.DonorSpace.Infrastructure.Commands;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Commands
{
    public abstract class CommandProcessorSpecification : SpecificationFor<CommandProcessor>
    {
        protected FakeCommand Command = new FakeCommand();
        protected ICommandHandler<FakeCommand> CommandHandler;
        protected ITransaction Transaction;
        protected IUnitOfWork UnitOfWork;

     
        protected virtual Func<Type, ICommandHandler> CommandHandlerFactory
        {
            get { return t => CommandHandler; }
        }

        protected CommandProcessorSpecification()
        {

            CommandHandler = SubstituteFor<ICommandHandler<FakeCommand>>();
            UnitOfWork = SubstituteFor<IUnitOfWork>();
            Transaction = SubstituteFor<ITransaction>();

            UnitOfWork.BeginTransaction().Returns(Transaction);

            SUT = new CommandProcessor(CommandHandlerFactory, UnitOfWork);
            
        }
    }
}
