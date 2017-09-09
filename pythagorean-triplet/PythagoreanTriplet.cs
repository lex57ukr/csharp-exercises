using System;
using System.Linq;
using System.Collections.Generic;


public class Triplet
{
    int A { get; }
    int B { get; }
    int C { get; }

    public Triplet(int a, int b, int c)
    {
        this.A = a;
        this.B = b;
        this.C = c;
    }

    public int Sum()
        => this.A + this.B + this.C;

    public int Product()
        => this.A * this.B * this.C;

    public bool IsPythagorean()
        => Pow2(this.A) + Pow2(this.B) == Pow2(this.C);

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
