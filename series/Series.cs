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
        => _digits.CheckSlice(sliceLength)
        .SliceMap(sliceLength, x => x)
        .ToArray();
}


static class ImmutableArrayExtensions
{
    public static ImmutableArray<TItem> CheckSlice<TItem>(
        this ImmutableArray<TItem> source,
        int size
    )
    {
        if (size > source.Length)
        {
            throw new ArgumentException("The slice exceeds the source.");
        }

        return source;
    }

    public static IEnumerable<TResult> SliceMap<TItem, TResult>(
        this ImmutableArray<TItem> source,
        int size,
        Func<TItem[], TResult> map
    ) => Enumerable
        .Range(0, count: source.Length + 1 - size)
        .Aggregate(
            ImmutableList<TResult>.Empty,
            (acc, i) => acc.Add(
                map(source.SliceAt(i, size))
            )
        );

    static TItem[] SliceAt<TItem>(
        this ImmutableArray<TItem> source, int i, int size
    )
    {
        var dest = new TItem [size];
        source.CopyTo(i, dest, 0, size);

        return dest;
    }
}