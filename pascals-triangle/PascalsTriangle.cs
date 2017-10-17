using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using static System.Linq.Enumerable;


public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
        => Range(0, rows).Aggregate(
            ImmutableList<IEnumerable<int>>.Empty,
            (acc, n) => acc.Add(Row(n))
        );

    private static IEnumerable<int> Row(int n)
        => Range(0, n).Aggregate(
            ImmutableList<int>.Empty.Add(1),
            (acc, i) => acc.Add(Next(acc.Last(), i, n))
        );

    private static int Next(int prev, int i, int row)
        => prev * (row - i) / (i + 1);
}
