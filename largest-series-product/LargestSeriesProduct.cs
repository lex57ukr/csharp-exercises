using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        if (span > digits.Length || span < 0)
        {
            throw new ArgumentException("Bad span.");
        }

        try
        {
            return digits
                .SliceMap(span, ToIntegers)
                .Select(Product)
                .DefaultIfEmpty(1)
                .Max();
        }
        catch (FormatException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    static IEnumerable<int> ToIntegers(IEnumerable<char> source)
        => source.Select(char.ToString).Select(int.Parse);

    static long Product(IEnumerable<int> source)
        => source.Aggregate((long) 1, (acc, n) => acc * n);
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
}

