using System;
using System.Linq;
using System.Collections.Generic;


public class Triplet
{
    readonly int[] _sides;

    public Triplet(int a, int b, int c)
        => _sides = new [] {a, b, c};

    public int Sum()
        => _sides.Sum();

    public int Product()
        => _sides.Aggregate(1, (acc, x) => acc * x);

    public bool IsPythagorean()
        => _sides.Take(2).Select(Pow2).Sum() == Pow2(_sides[2]);

    public static IEnumerable<Triplet> Where(
        int maxFactor,
        int minFactor = 1,
        int sum = 0
    ) => GenerateTriplesFromFactors(
        minFactor,
        maxFactor,
        sum == 0 ? PythagoreanOnly() : PythagoreanWithSum(sum)
    );

    static IEnumerable<Triplet> GenerateTriplesFromFactors(
        int min,
        int max,
        Func<Triplet, bool> predicate
    ) => (
        from a in Range(min, max)
        from b in Range(a, max)
        from c in Range(b, max)
        select new Triplet(a, b, c)
    ).Where(predicate);

    static IEnumerable<int> Range(int min, int max)
        => Enumerable.Range(min, count: max - min + 1);

    static Func<Triplet, bool> PythagoreanOnly()
        => (t => t.IsPythagorean());

    static Func<Triplet, bool> PythagoreanWithSum(int sum)
        => (t => t.IsPythagorean() && sum == t.Sum());

    static int Pow2(int x) => (int) Math.Pow(x, 2);
}
