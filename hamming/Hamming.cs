using System;
using System.Linq;
using System.Collections.Generic;


public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand)
    {
        if (firstStrand.Length != secondStrand.Length)
        {
            throw new ArgumentException(
                "Strands of equal lenghts only."
            );
        }

        return firstStrand
            .Zip(secondStrand, (x, y) => x != y)
            .Count(isDiff => isDiff);
    }
}
