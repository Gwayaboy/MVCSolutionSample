using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain
{
    public enum AuthenticationType
    {
        OnPremise,
        OffPremise
    }

    public class SomeEnumeration : Enumeration<SomeEnumeration>
    {
        public static SomeEnumeration FirstItem = new SomeEnumeration(1,"This is my first");
        public static SomeEnumeration SecondItem = new SomeEnumeration(2,"This is my second");
        public static SomeEnumeration LastItem = new SomeEnumeration(3,"This is my last and everything");

        public SomeEnumeration(int value, string displayName) : base(value, displayName){ }
    }
}