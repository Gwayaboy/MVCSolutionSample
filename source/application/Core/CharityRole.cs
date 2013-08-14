using Panzea.DonorSpace.Core.Domain.Base;

namespace Panzea.DonorSpace.Core
{
    public class  CharityRole : Enumeration<CharityRole,int>
    {
        public CharityRole(int value, string displayName) : base(value, displayName)
        {
        }

        public static CharityRole Administrator = new CharityRole(1,"Administrator");
        public static CharityRole Donor = new CharityRole(2, "Donor");
    }
}