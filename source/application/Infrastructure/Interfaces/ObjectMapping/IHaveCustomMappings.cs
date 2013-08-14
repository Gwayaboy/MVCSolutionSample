using System;
using System.Security.Principal;
using AutoMapper;

namespace Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping
{
    public interface IHaveCustomMappings
    {
        Func<IPrincipal> UserIdentityProvider { get; set; }
        void CreateMappings(IConfiguration configuration);

    }
}