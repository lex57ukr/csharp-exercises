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

        return EnumPrimes(limit).ToArray();
    }

    static IEnumerable<int> EnumPrimes(int limit)
    {
        var candidates = Enumerable
                .Repeat(element: true, count: limit + 1)
                .ToArray();

        void MarkMultiplesOf(int prime)
        {
            for (int i = prime * 2; i < candidates.Length; i += prime)
            {
                candidates[i] = false;
            }
        }

        for (int i = 2; i < candidates.Length; ++i)
        {
            if (candidates[i])
            {
                MarkMultiplesOf(i);
                yield return i;
            }
        }
    }
}
