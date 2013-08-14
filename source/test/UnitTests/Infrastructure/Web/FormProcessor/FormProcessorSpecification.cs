using Intrigma.DonorSpace.Acceptance.Fakes;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Logging;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Web.FormProcessor
{
    public abstract class FormProcessorSpecification : SpecificationFor<DonorSpace.Infrastructure.Web.FormProcessor>
    {
        protected FakeForm Form = new FakeForm();
        protected FakeCommand Command = new FakeCommand();
        protected ExecutionResult ExecutionResult;
        protected ILogger Logger { get; private set; }
        protected IModelMapper ModelMapper { get; private set; }
        protected ICommandProcessor CommandProcessor { get; private set; }

        protected FormProcessorSpecification()
        {
            Logger = SubstituteFor<ILogger>();
            ModelMapper = SubstituteFor<IModelMapper>();
            CommandProcessor = SubstituteFor<ICommandProcessor>();

            ExecutionResult = new ExecutionResult(Command);

            ModelMapper.MapFormToCommand(Form).Returns(Command);
            CommandProcessor.Execute(Command).Returns(ExecutionResult);
        }
    }
}
