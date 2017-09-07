using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class Matrix
{
    readonly int[,] _data;

    public Matrix(string input)
        => _data = input
        .Split('\n')
        .Aggregate(
            ImmutableList<IEnumerable<int>>.Empty,
            ParseLine
        ).To2DArray();

    public int Rows
        => _data.GetLength(0);

    public int Cols
        => _data.GetLength(1);

    public int[] Row(int row) => Enumerable
        .Range(0, count: this.Cols)
        .Select(col => _data[row, col])
        .ToArray();

    public int[] Col(int col) => Enumerable
        .Range(0, count: this.Rows)
        .Select(row => _data[row, col])
        .ToArray();

    static ImmutableList<IEnumerable<int>> ParseLine(
        ImmutableList<IEnumerable<int>> acc,
        string line
    ) => acc.Add(line.Split(' ').Select(int.Parse));
}

static class EnumerableExtensions
{
    public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var data = source
            .Select(x => x.ToArray())
            .ToArray();

        var res = new T[data.Length, data.Max(x => x.Length)];
        for (var i = 0; i < data.Length; i++)
        {
            for (var j = 0; j < data[i].Length; ++j)
            {
                res[i,j] = data[i][j];
            }
        }

        return res;
    }
}
