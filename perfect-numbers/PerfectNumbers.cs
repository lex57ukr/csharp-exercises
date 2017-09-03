using System;
using System.Linq;
using System.Collections.Generic;


public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}


public static class PerfectNumbers
{
    static readonly (
        Func<int, int, bool> matcher, Classification kind
    )[] Classes = {
        ((a, n) => a == n, Classification.Perfect),
        ((a, n) => a > n, Classification.Abundant),
        ((a, n) => a < n, Classification.Deficient),
    };

    public static Classification Classify(int number)
        => number.AliquotSum().ClassOf(number);

    static int AliquotSum(this int number)
        => number.Factors().Sum();

    static IEnumerable<int> Factors(this int number)
        => Enumerable
            .Range(1, count: number - 1)
            .Where(n => number % n == 0);

    static Classification ClassOf(this int aliquotSum, int number)
        => Classes
            .Where(c => c.matcher(aliquotSum, number))
            .Select(c => c.kind)
            .First();
}
