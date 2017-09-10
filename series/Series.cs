using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class Series
{
    readonly ImmutableArray<int> _digits;

    public Series(string numbers)
        => _digits = numbers
        .Select(char.ToString)
        .Select(int.Parse)
        .ToImmutableArray();

    public int[][] Slices(int sliceLength)
        => _digits
        .CheckSlice(sliceLength)
        .SliceMap(sliceLength, x => x.ToArray())
        .ToArray();
}


static class EnumerableExtensions
{
    public static IEnumerable<TResult> SliceMap<TItem, TResult>(
        this IEnumerable<TItem> source,
        int size,
        Func<IEnumerable<TItem>, TResult> map
    ) => Enumerable
        .Range(0, count: source.Count() + 1 - size)
        .Aggregate(
            ImmutableList<TResult>.Empty,
            (acc, i) => acc.Add(
                map(source.Skip(i).Take(size))
            )
        );

    public static IEnumerable<TItem> CheckSlice<TItem>(
        this IEnumerable<TItem> source,
        int size
    )
    {
        if (size > source.Count())
        {
            throw new ArgumentException("The slice exceeds the source.");
        }

        return source;
    }
}