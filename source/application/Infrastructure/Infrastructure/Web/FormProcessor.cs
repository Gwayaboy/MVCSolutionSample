using Panzea.DonorSpace.Core.Interfaces.Commands;
using Panzea.DonorSpace.Infrastructure.ApplicationMessage;
using Panzea.DonorSpace.Infrastructure.Interfaces;
using Panzea.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Panzea.DonorSpace.Infrastructure.Logging;
using Seterlund.CodeGuard;

namespace Panzea.DonorSpace.Infrastructure.Web
{
    public class FormProcessor : IFormProcessor
    {
        private readonly ILogger _logger;
        readonly IModelMapper _mapper;
        readonly ICommandInvoker _commandInvoker;
        private ExecutionResult _executionResult;

        public FormProcessor(ILogger logger, IModelMapper mapper, ICommandInvoker commandInvoker)
        {
            _logger = logger;
            _mapper = mapper;
            _commandInvoker = commandInvoker;
        }

        public ExecutionResult Execute<TForm>(TForm form)
            where TForm : class
        {
            Guard.That(() => form).IsNotNull();
            _executionResult = new ExecutionResult();

            if (_executionResult.IsSuccessFull)
            {
                dynamic command = _mapper.MapFormToCommand(form);
                _executionResult = _commandInvoker.Execute(command);
            }

            _logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        
    }
}