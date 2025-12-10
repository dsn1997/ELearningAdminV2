using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Helpers;

public static class ListExtension
{
    public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int nSize = 100)
    {
        for (int i = 0; i < locations.Count; i += nSize)
        {
            yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
        }
    }
    
    public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable ?? Enumerable.Empty<T>();
    }
}