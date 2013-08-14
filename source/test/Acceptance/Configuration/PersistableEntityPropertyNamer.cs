using System;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using Intrigma.DonorSpace.Infrastructure.Extensions;

namespace Intrigma.DonorSpace.Acceptance.Configuration
{
    public class PersistableEntityPropertyNamer : SequentialPropertyNamer
    {
        public PersistableEntityPropertyNamer(IReflectionUtil reflectionUtil)
            : base(reflectionUtil)
        {
        }

        protected override Guid GetGuid(MemberInfo memberInfo)
        {
            return memberInfo.EntityHasIdOfType<Guid>() ? Guid.Empty : base.GetGuid(memberInfo);
        }

        protected override int GetInt32(MemberInfo memberInfo)
        {
            return memberInfo.EntityHasIdOfType<int>() ? 0 : base.GetInt32(memberInfo);
        }

       
    }

}