using System;
using AutoMapper;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping.Profiles
{
    public class AutoMapperCustomMappingsProfile : Profile
    {
        private static Func<Type, object> _serviceResolver = AutofacExtensions.HttpRequestScopedFactory;

        public static void WithServiceResolver(Func<Type, object> serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        protected override void Configure()
        {
            AssemblyScanner
                .GetExportedTypesFromWebAssembly()
                .GetConcreteTypesImplementing<IHaveCustomMappings>()
                .ResolveConcreteTypesAs<IHaveCustomMappings>(_serviceResolver)
                .Each(viewModel => viewModel.CreateMappings(Mapper.Configuration));
            

        }
    }
}