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
        if (list1.Count < list2.Count)
        {
            return Find(list1, list2, SublistType.Sublist);
        }
        else if (list1.Count > list2.Count)
        {
            return Find(list2, list1, SublistType.Superlist);
        }

        return Find(list1, list2, SublistType.Equal);
    }

    private static SublistType Find<T>(List<T> x, List<T> y, SublistType whenFound)
    {
        var comparer = EqualityComparer<T>.Default;

        int i = 0;
        while (i + x.Count <= y.Count)
        {
            int ix = 0, iy = i++;
            while (ix < x.Count && iy < y.Count && comparer.Equals(x[ix], y[iy]))
            {
                ++ix;
                ++iy;
            }

            if (ix == x.Count)
            {
                return whenFound;
            }
        }

        return SublistType.Unequal;
    }
}
