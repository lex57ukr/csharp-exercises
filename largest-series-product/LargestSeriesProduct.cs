using System;
using System.Linq;
using System.Collections.Generic;


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
                .Slices(span)
                .Select(Integers)
                .Select(Product)
                .Max();
        }
        catch (FormatException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    static IEnumerable<string> Slices(this string source, int span)
        => Enumerable
        .Range(0, count: source.Length + 1 - span)
        .Select(i => source.Substring(i, span));

    static IEnumerable<int> Integers(IEnumerable<char> source)
        => source.Select(char.ToString).Select(int.Parse);

    static long Product(IEnumerable<int> source)
        => source.Aggregate((long) 1, (acc, n) => acc * n);
}
