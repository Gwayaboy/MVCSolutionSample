using System;
using System.Collections.Generic;

namespace Intrigma.DonorSpace.Infrastructure.Extensions
{
    public static class Traverse
    {
        public static IEnumerable<T> Across<T>(T first, Func<T, T> next)
            where T : class
        {
            var item = first;
            while (item != null)
            {
                yield return item;
                item = next(item);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }

            return enumerable;
        }
    }
}