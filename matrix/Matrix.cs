using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class Matrix
{
    readonly int[][] _data;

    public Matrix(string input)
        => _data = input
        .Split('\n')
        .Where(s => ! string.IsNullOrWhiteSpace(s))
        .Aggregate(
            ImmutableList<int[]>.Empty,
            ParseLine
        ).ToArray();

    public int Rows
        => _data.Length;

    public int Cols
        => _data.Select(c => c.Length).FirstOrDefault();

    public int[] Row(int row)
        => _data[row];

    public int[] Col(int col) => Enumerable
        .Range(0, count: this.Rows)
        .Select(row => _data[row][col])
        .ToArray();

    static ImmutableList<int[]> ParseLine(
        ImmutableList<int[]> acc,
        string line
    ) => acc.Add(
        line.Split(' ').Select(int.Parse).ToArray()
    );
}
