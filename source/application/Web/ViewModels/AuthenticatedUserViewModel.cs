using System;
using AutoMapper;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;
using System.Security.Principal;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class AuthenticatedUserViewModel : IHaveCustomMappings
    {

        public int CharityId { get; set; }
        public Role Role { get; set; }
        public Func<IPrincipal> UserIdentityProvider { get; set; }

        public bool IsAdministrator
        {
            get { return Role == Role.Administrator; }
        }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration
                .CreateMap<Charity, AuthenticatedUserViewModel>()
                .ForMember(viewModel => viewModel.CharityId, o => o.MapFrom(charity => charity.Id))
                .ForMember(viewModel => viewModel.Role, o => o.MapFrom(charity => ResolveRole(charity)))
                .IgnoreAllNonExisting();
        }

        private Role ResolveRole(Charity charity)
        {
            var userName = UserIdentityProvider().Identity.Name;
            if (charity.IsAdministrator(userName))
            {
                return Role.Administrator;
            }

            return Role.Donor;

        }
    }
}