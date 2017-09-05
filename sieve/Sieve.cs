using System;
using System.Linq;
using System.Collections.Generic;


public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 2)
        {
            throw new ArgumentOutOfRangeException();
        }

        return Enumerable
            .Range(2, count: limit - 1)
            .Where(IsPrime)
            .ToArray();
    }

    static bool IsPrime(int number) => Enumerable
        .Range(2, count: (int) Math.Sqrt(number) - 1)
        .All(x => number % x != 0);
}
