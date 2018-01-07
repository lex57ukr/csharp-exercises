using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static System.Math;
using static System.Linq.Enumerable;


public static class RomanNumeralExtension
{
    private static readonly ImmutableArray<(int Arabic, string Roman)>
        Numerals = new [] {
            (1,    "I"),
            (5,    "V"),
            (10,   "X"),
            (50,   "L"),
            (100,  "C"),
            (500,  "D"),
            (1000, "M"),
        }.ToImmutableArray();

    public static string ToRoman(this int value)
    {
        var numerals = value.ToDecimalParts().Select(AsNumeral);
        return string.Join(",", numerals);
    }

    private static IEnumerable<int> ToDecimalParts(this int value)
        => Range(0, CountDecimalPlaces(value))
            .Reverse()
            .Select(Pow10)
            .Aggregate(
                (parts: ImmutableList<int>.Empty, value: value),
                (acc, p) => (
                    AddNonZeroPart(acc.parts, GetPart(acc.value, p)),
                    value % p
                )
            ).parts;

    private static int CountDecimalPlaces(int n)
        => (int) Log10(n) + 1;

    private static int Pow10(int n)
        => (int) Pow(10, n);

    private static int GetPart(int value, int pow)
        => (value / pow) * pow;

    private static ImmutableList<int> AddNonZeroPart(
        ImmutableList<int> acc,
        int part
    ) => 0 != part ? acc.Add(part) : acc;

    private static string AsNumeral(int n)
    {
        return n.ToString();
    }
}
