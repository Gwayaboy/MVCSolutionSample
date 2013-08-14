using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Helper.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.Web;
using Intrigma.DonorSpace.Infrastructure.Logging;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.Web
{
    public class FormProcessor : IFormProcessor
    {
        private readonly ILogger _logger;
        readonly IModelMapper _mapper;
        readonly ICommandProcessor _commandProcessor;
        private ExecutionResult _executionResult;

        public FormProcessor(ILogger logger,
                             IModelMapper mapper,
                             ICommandProcessor commandProcessor)
        {
            _logger = logger;
            _mapper = mapper;
            _commandProcessor = commandProcessor;
        }

        public ExecutionResult Execute<TForm>(TForm form)
            where TForm : class
        {
            Guard.That(() => form).IsNotNull();

            dynamic command = _mapper.MapFormToCommand(form);
            _executionResult = _commandProcessor.Execute(command);

            _logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        
    }
}