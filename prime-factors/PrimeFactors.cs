using System;
using System.Linq;
using System.Collections.Generic;


public static class PrimeFactors
{
    public static int[] Factors(long number)
        => EnumFactors(number).ToArray();

    private static IEnumerable<int> EnumFactors(long number)
    {
        var n = number;
        var factor = 2;

        while (n != 1)
        {
            if (n % factor != 0)
            {
                factor++;
            }
            else
            {
                n /= factor;
                yield return factor;
            }
        }
    }
}