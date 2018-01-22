using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static System.Linq.Enumerable;


public static class Minesweeper
{
    public static string[] Annotate(string[] input)
    {
        if (input.Length == 0)
        {
            return input;
        }

        var hits = Bombs(input)
            .SelectMany(Hits)
            .GroupBy(x => x)
            .Select(x => (Point: x.Key, Hits: x.Count()));

        var annotations = Points(input).GroupJoin(
            hits,
            x => x,
            h => h.Point,
            (x, h) => new { Point = x, Hits = h }
        ).SelectMany(
            x => x.Hits.DefaultIfEmpty(),
            (x, h) => new { x.Point, Char = Render(input, x.Point, h.Hits) }
        );

        var build = annotations.Aggregate(
            (Row: 0, Rows: ImmutableList<string>.Empty, Chars: ImmutableList<char>.Empty),
            (acc, x) => {
                var (row, rows, chars) = acc;

                if (row != x.Point.Row)
                {
                    rows  = Finalize(rows, chars);
                    chars = ImmutableList<char>.Empty;
                }

                chars = chars.Add(x.Char);
                return (x.Point.Row, rows, chars);
            }
        );

        return Finalize(build.Rows, build.Chars).ToArray();
    }

    private static IEnumerable<(int Row, int Column)> Hits((int Row, int Column) point)
    {
        return Range(-1, 3).SelectMany(
            _ => Range(-1, 3),
            (x, y) => (point.Row + x, point.Column + y)
        );
    }

    private static IEnumerable<(int Row, int Column)> Points(string[] input)
    {
        return Range(0, input.Length).SelectMany(
            row => Range(0, input[row].Length),
            (row, col) => (row, col)
        );
    }

    private static IEnumerable<(int Row, int Column)> Bombs(string[] input)
        => Points(input).Where(x => IsBomb(input, x));

    private static bool IsBomb(string[] input, (int Row, int Column) point)
        => input[point.Row][point.Column] == '*';

    private static char Render(string[] input, (int Row, int Column) point, int hits)
    {
        return hits == 0 || IsBomb(input, point)
            ? input[point.Row][point.Column]
            : ToChar(hits);
    }

    private static char ToChar(int hits)
        => (char) ((int) '0' + hits);

    private static ImmutableList<string> Finalize(
        ImmutableList<string> rows,
        IEnumerable<char> chars
    ) => rows.Add(string.Concat(chars));
}
