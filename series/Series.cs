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
        => ThrowIfSlicingImpossible(sliceLength)
        .IndexSlices(sliceLength)
        .Aggregate(
            ImmutableList<int[]>.Empty,
            (acc, i) => acc.Add(SliceAt(i, sliceLength))
        ).ToArray();

    Series ThrowIfSlicingImpossible(int size)
    {
        if (size > _digits.Length)
        {
            throw new ArgumentException();
        }

        return this;
    }

    IEnumerable<int> IndexSlices(int size)
        => Enumerable
        .Range(0, count: _digits.Length + 1 - size);

    int[] SliceAt(int i, int size)
    {
        var dest = new int [size];
        _digits.CopyTo(i, dest, 0, size);

        return dest;
    }
}
