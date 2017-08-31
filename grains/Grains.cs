using System;
using System.Linq;
using System.Collections.Generic;


public static class Grains
{
    const int MaxSquares = 64;

    public static ulong Square(int n)
        => TwoInPower(n - 1);

    public static ulong Total()
        => TwoInPower(MaxSquares) - 1;

    static ulong TwoInPower(int n) => Enumerable
        .Range(1, n)
        .Aggregate((ulong) 1, (acc, _) => acc * 2);
}
