using System;
using System.Collections.Generic;
using System.Linq;

namespace NResult.Tests
{
    public static class ToIndexExtension
    {
        public static IEnumerable<object[]> ToIndex<T>(this ICollection<T> src, int maxCount = int.MaxValue) => Enumerable
            .Range(0, Math.Min(src.Count, maxCount))
            .Select(x => new object[] { x });
    }
}
