using System;
using Intrigma.DonorSpace.Core.Interfaces.Commands;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using Seterlund.CodeGuard;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping
{
    public class ModelMapper : IModelMapper
    {
        readonly IMapper _mapper;

        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ICommand MapFormToCommand<TForm>(TForm form)
        {
            var formType = typeof (TForm);

            var mappingConfiguration = _mapper.GetMappingConfigurationMatching(formType);
            if (mappingConfiguration == null)
            {
                throw new ArgumentException(string.Format("There is no Command associated with Form {0}", formType.Name));
            }
            var commandType = mappingConfiguration.DestinationType;
            return (ICommand)Map(form, commandType);
        }

        public virtual object Map(object source, Type destinationType)
        {

            Guard.That(source).IsNotNull();

            return _mapper.Map(source, source.GetType(), destinationType);
        }

        public TViewModel MapViewModel<TViewModel>(object source)
        {
            if (source == null) return default(TViewModel);

            return (TViewModel) Map(source, typeof (TViewModel));
        }

    

    }
}