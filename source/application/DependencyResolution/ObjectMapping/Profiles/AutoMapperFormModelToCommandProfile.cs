using AutoMapper;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping.Profiles
{
    public class AutoMapperFormToCommandProfile : Profile
    {
        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromWebAssembly()
                .GetSourceAndDestinationTypesFromMarkerMappingToCommandInterface(typeof (IMapToCommand<>))
                .CreateMap();
        }
    }
}