using System;
using Intrigma.DonorSpace.Acceptance.Fakes;
using Intrigma.DonorSpace.Acceptance.Specification;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using NSubstitute;

namespace Intrigma.DonorSpace.UnitTests.Infrastructure.ObjectMapping
{
    public abstract class ModelMapperSpecification : SpecificationFor<ModelMapper>
    {
        protected readonly FakeForm Form = new FakeForm();
        protected readonly FakeCommand Command = new FakeCommand();

        protected static Type FormType = typeof(FakeForm);
        protected static Type CommandType = typeof(FakeCommand);
        protected readonly IMappingConfiguration NullMappingConfiguration = null;

        
        protected ModelMapperSpecification()
        {
            Mapper = SubstituteFor<IMapper>();
            MappingConfiguration = SubstituteFor<IMappingConfiguration>();

            MappingConfiguration = SubstituteFor<IMappingConfiguration>();
            
            Mapper
                .GetMappingConfigurationMatching(FormType)
                .Returns(MappingConfiguration);
        }

        protected IMappingConfiguration MappingConfiguration { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}