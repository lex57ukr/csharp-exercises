using System;
using System.Linq;
using System.Collections.Generic;


public static class SumOfMultiples
{
    public static int To(IEnumerable<int> factors, int max)
    {
        IEnumerable<int> Multiples(int factor) => Enumerable
            .Range(1, max - 1)
            .Where(n => n % factor == 0);

        return factors
            .SelectMany(Multiples)
            .Distinct()
            .Sum();
    }
}
