using System.Collections.Generic;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain
{
    public class Role : Enumeration<Role>
    {

        internal ICollection<Administrator> AdministratorCollection { get; set; }
        internal ICollection<Donor> DonorCollection { get; set; }
        
        public Role() { }
        
        public Role(int value, string displayName) : base(value, displayName) { }

        public static Role Administrator = new Role(1,"Administrator");
        public static Role Donor = new Role(2, "Donor");
        
    }
}