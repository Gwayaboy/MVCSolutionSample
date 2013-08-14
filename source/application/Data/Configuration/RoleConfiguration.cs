using Intrigma.DonorSpace.Core.Domain;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Configuration
{
    public class RoleConfiguration : EnumerationConfiguration<Role>
    {

        protected override string TableName
        {
            get { return "webpages_Roles"; }
        }

        protected override string KeyColumnName
        {
            get { return "RoleId"; }
        }

        protected override string NameColumnName
        {
            get { return "RoleName"; }
        }
    }
}