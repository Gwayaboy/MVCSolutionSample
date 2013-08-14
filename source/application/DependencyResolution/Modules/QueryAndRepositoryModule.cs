using System;
using Autofac;
using Intrigma.DonorSpace.Core.Interfaces.Data.Read;
using Intrigma.DonorSpace.Core.Interfaces.Data.Write;
using Intrigma.DonorSpace.Infrastructure.Data.EF.Context;
using AssemblyScanner = Intrigma.DonorSpace.Infrastructure.Extensions.AssemblyScanner;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Modules
{
    public class QueryAndRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Type openGenericRepositoryType = typeof (IRepository<>),
                 openGenericQueryType = typeof (IQuery<>);

            builder
                .RegisterAssemblyTypes(AssemblyScanner.FromAssemblyContaining<DataContext>())
                .Where(t => t.IsClosedTypeOf(openGenericRepositoryType) ||
                            t.IsClosedTypeOf(openGenericQueryType))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}