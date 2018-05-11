using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;


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

    private static SublistType Find<T>(List<T> a, List<T> b, SublistType type)
    {
        bool Match(int n)
            => a.Zip(b.Skip(n), EqualityComparer<T>.Default.Equals).All(x => x);

        return Range(0, b.Count - a.Count + 1).Select(Match).Any(x => x)
            ? type
            : SublistType.Unequal;
    }
}
