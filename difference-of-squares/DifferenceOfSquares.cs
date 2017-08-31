using System;
using System.Linq;
using System.Collections.Generic;


public static class Squares
{
    public static int SquareOfSums(int max) => max
        .NaturalNumbers()
        .Sum()
        .Square();

    public static int SumOfSquares(int max) => max
        .NaturalNumbers()
        .Select(Square)
        .Sum();

    public static int DifferenceOfSquares(int max)
        => SquareOfSums(max) - SumOfSquares(max);

    static IEnumerable<int> NaturalNumbers(this int max)
        => Enumerable.Range(start: 1, count: max);

    static int Square(this int value)
        => value * value;
}
