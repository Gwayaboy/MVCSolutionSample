using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain.MemberShip
{
    public class OAuthMembership : AggregateRoot
    {
        public string Provider { get; protected set; }
        public string ProviderUserId { get; protected set; }
    }
}