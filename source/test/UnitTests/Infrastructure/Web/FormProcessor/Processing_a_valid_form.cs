using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.Web.FormProcessor
{
    public class Processing_a_valid_form : FormProcessorSpecification
    {
       public void When_processing_form()
        {
            SUT.Execute(Form);
        }

        public void Then_the_command_should_be_mapped_from_the_form()
        {
            ModelMapper.Received(OnlyOnce).MapFormToCommand(Form);
        }

        public void AndThen_the_command_should_be_processed()
        {
            CommandProcessor.Received(OnlyOnce).Execute(Command);
        }


        public void AndThen_the_execution_result_should_be_logged()
        {
            Logger.Received(OnlyOnce).Info(ExecutionResult.ToString());
        }

    }
}