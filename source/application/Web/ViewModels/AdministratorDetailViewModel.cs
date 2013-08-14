using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using AutoMapper;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping;
using Intrigma.DonorSpace.Infrastructure.Extensions;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class AdministratorDetailViewModel : IHaveCustomMappings
    {
        public int CharityId { get; set; }
        public int Id { get; set; }

        public Func<IPrincipal> UserIdentityProvider { get; set; }

        [DisplayName("User name")]
        public string UserName { get; set; }
      
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public bool IsCurrentAuthenticatedUser { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration
               .CreateMap<Administrator, AdministratorDetailViewModel>()
               .ForMember(viewModel => viewModel.IsCurrentAuthenticatedUser,
                          o => o.MapFrom(administrator => IsCurrentUser(administrator)))
               .IgnoreAllNonExisting();

            configuration
                .CreateMap<Charity, IEnumerable<AdministratorDetailViewModel>>()
                .ConvertUsing(ConvertToAdministratorDetailViewModels);

           
        }

        private static IEnumerable<AdministratorDetailViewModel> ConvertToAdministratorDetailViewModels(Charity charity)
        {
            return
                Mapper
                    .Map<IEnumerable<Administrator>, IEnumerable<AdministratorDetailViewModel>>(charity.Administrators)
                    .ForEach(a => a.CharityId = charity.Id);


        }

        private bool IsCurrentUser(Administrator administrator)
        {
            return administrator.UserName == UserIdentityProvider().Identity.Name;
        }
    }

}