using System;
using System.Collections.Generic;


public static class AccumulateExtensions
{
    public static IEnumerable<TResult> Accumulate<TSource, TResult>(
        this IEnumerable<TSource> collection,
        Func<TSource, TResult> func
    )
    {
        foreach (var item in collection)
        {
            yield return func(item);
        }
    }
}
