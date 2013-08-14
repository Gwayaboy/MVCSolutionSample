using System;
using AutoMapper;
using Humanizer;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.ObjectMapping.Converters
{
    public class EnumTypeConverter : TypeConverter<Enum, string>
    {
        protected override string ConvertCore(Enum source)
        {
            return source.ToString().Humanize(LetterCasing.Title);
        }
    }
}