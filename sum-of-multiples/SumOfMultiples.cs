using System;
using System.Linq;
using System.Collections.Generic;


public static class SumOfMultiples
{
    public static int To(IEnumerable<int> multiples, int max)
    {
        return multiples
            .SelectMany(factor => MultiplesOf(factor, max))
            .Distinct()
            .Sum();
    }

    static IEnumerable<int> MultiplesOf(int factor, int max)
    {
        for ( int n = factor; n < max; n += factor )
        {
            yield return n;
        }
    }
}