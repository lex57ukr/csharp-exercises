using System;
using System.Collections.Generic;


public enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

public static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2)
        where T : IComparable
    {
        if (list1.Count > list2.Count)
        {
            return Find(list2, list1, SublistType.Superlist);
        }

        var type = list1.Count == list2.Count
            ? SublistType.Equal
            : SublistType.Sublist;

        return Find(list1, list2, type);
    }

    private static SublistType Find<T>(List<T> x, List<T> y, SublistType type)
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = 0; i + x.Count <= y.Count; ++i)
        {
            int ix = 0, iy = i;
            while (ix < x.Count && comparer.Equals(x[ix], y[iy]))
            {
                ++ix;
                ++iy;
            }

            if (ix == x.Count)
            {
                return type;
            }
        }

        return SublistType.Unequal;
    }
}
