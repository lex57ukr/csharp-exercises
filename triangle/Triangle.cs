using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

using TriangleSides = System.Collections.Immutable.ImmutableArray<decimal>;

public enum TriangleKind
{
    Equilateral,
    Isosceles,
    Scalene
}


public static class Triangle
{
    static readonly (Func<int, bool> matcher, TriangleKind kind)[] Classes = {
        (IsEquilateral, TriangleKind.Equilateral),
        (IsIsosceles, TriangleKind.Isosceles),
        (IsScalene, TriangleKind.Scalene)
    };

    static readonly Func<TriangleSides, bool>[] Validators = {
        HasThreeSides,
        AllSidesPositive,
        HasBasicInequality,
    };

    public static TriangleKind Kind(decimal side1, decimal side2, decimal side3)
        => ComposeSides(side1, side2, side3)
            .ThrowIfNotTriangle()
            .CountDistinctSides()
            .Classify();

    static TriangleSides ComposeSides(params decimal[] side)
        => side.ToImmutableArray();

    static TriangleSides ThrowIfNotTriangle(this TriangleSides sides)
    {
        if ( ! sides.AreForValidTriangle())
        {
            throw new TriangleException();
        }

        return sides;
    }

    static bool AreForValidTriangle(this TriangleSides sides)
        => Validators
            .Select(validate => validate(sides))
            .All(valid => valid);

    static int CountDistinctSides(this TriangleSides sides)
        => sides.Distinct().Count();

    static TriangleKind Classify(this int distinctSides)
        => Classes
            .Where(c => c.matcher(distinctSides))
            .Select(c => c.kind)
            .First();

    static bool IsEquilateral(int distinctSides)
        => distinctSides == 1;

    static bool IsIsosceles(int distinctSides)
        => distinctSides == 2;

    static bool IsScalene(int distinctSides)
        => distinctSides == 3;

    static bool HasThreeSides(TriangleSides sides)
        => sides.Length == 3;

    static bool AllSidesPositive(TriangleSides sides)
        => sides.Count(s => s > 0) == sides.Length;

    static bool HasBasicInequality(TriangleSides sides)
    {
        var indexes = Enumerable
            .Range(0, sides.Length)
            .ToImmutableHashSet();

        (int x, IEnumerable<int> other) ToCheck(int sideIndex)
            => (x: sideIndex, other: indexes.Remove(sideIndex));

        return indexes
            .Select(ToCheck)
            .Select(i => sides.CalcLengths(i))
            .Select(IsNotDegenerate)
            .All(valid => valid);
    }

    static IEnumerable<decimal> Lengths(
        this TriangleSides sides,
        IEnumerable<int> indexes
    ) => indexes.Select(i => sides[i]);

    static (decimal length, decimal sumOfLengths) CalcLengths(
        this TriangleSides sides,
        (int x, IEnumerable<int> other) index
    ) => (
        length:       sides[index.x],
        sumOfLengths: sides.Lengths(index.other).Sum()
    );

    static bool IsNotDegenerate(
        (decimal length, decimal sumOfLengths) check
    ) => check.length < check.sumOfLengths;
}


public class TriangleException
    : Exception
{
}
