using System;
using Autofac.Builder;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution
{
    public static class  AutoFacRegistrationExtensions
    {
        public static IRegistrationBuilder<T, TSimpleActivatorData, TSingleRegistrationStyle> ConfigureWith<T, TSimpleActivatorData, TSingleRegistrationStyle>(this IRegistrationBuilder<T, TSimpleActivatorData, TSingleRegistrationStyle> registration,
                                            Action<IRegistrationBuilder<T, TSimpleActivatorData, TSingleRegistrationStyle>> action)
        {
            action(registration);
            return registration;
        }
    }
}