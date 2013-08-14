using System.ComponentModel;
using System.Web.Mvc;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Infrastructure.Interfaces.ObjectMapping;

namespace Intrigma.DonorSpace.Web.ViewModels
{
    public class CharityViewModel : IMapFromDomain<Charity>
    {
        [HiddenInput]
        public int Id { get; set; }

        public AuthenticationType AuthenticationType { get; set; }

        [DisplayName("Site Name")]
        public string SiteName { get; set; }

        [DisplayName("Site Description")]
        public string Description { get; set; }

        [DisplayName("Web Service URL")]
        public string WebServiceUrl { get; set; }
    }
}