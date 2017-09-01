using System;
using System.Collections.Generic;


public static class Strain
{
    public static IEnumerable<T> Keep<T>(
        this IEnumerable<T> collection,
        Func<T, bool> predicate
    )
    {
        foreach (var item in collection)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> Discard<T>(
        this IEnumerable<T> collection,
        Func<T, bool> predicate
    ) => collection.Keep(predicate.Not());

    static Func<T, bool> Not<T>(this Func<T, bool> predicate)
        => (x => ! predicate(x));
}
