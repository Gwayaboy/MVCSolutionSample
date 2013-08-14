using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.ObjectMapping
{
    public class Mapping_successfully_a_Form_to_its_corresponding_Command : ModelMapperSpecification
    {
        public void Given_the_Form_is_mapped_to_a_Command()
        {
            MappingConfiguration.DestinationType.Returns(CommandType);
            Mapper.Map(Form, FormType, CommandType).Returns(Command);
        }

        public  void When_mapping_a_form_to_its_corresponding_command()
        {
            SUT.MapFormToCommand(Form);
        }

        public void Then_mapper_should_retrieve_the_mapping_configuration_for_the_form()
        {
            Mapper
                .Received(OnlyOnce)
                .GetMappingConfigurationMatching(FormType);
        }

        public void AndThen_mapper_should_the_form_to_its_corresponding_command()
        {
            Mapper
                .Received(OnlyOnce)
                .Map(Form, FormType, CommandType);
        }

    }
}