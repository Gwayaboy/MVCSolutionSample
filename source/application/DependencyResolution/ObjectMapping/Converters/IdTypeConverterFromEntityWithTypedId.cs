using AutoMapper;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping.Converters
{
    public class IdTypeConverterFromEntityWithTypedId<TId> : TypeConverter<EntityWithTypedId<TId>, TId>
    {
       
        protected override TId ConvertCore(EntityWithTypedId<TId> source)
        {
            return source == null ? default(TId) : source.Id;
        }
    }
}