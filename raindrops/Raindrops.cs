using System;
using System.Linq;


public static class Raindrops
{
    static readonly (int factor, string code)[] Codes = new [] {
        (factor: 3, code: "Pling"),
        (factor: 5, code: "Plang"),
        (factor: 7, code: "Plong"),
    };

    public static string Convert(int number)
    {
        var codes = Codes
            .Where(c => c.factor <= number)
            .Where(c => number % c.factor == 0)
            .Select(c => c.code)
            .DefaultIfEmpty(number.ToString());

        return String.Concat(codes);
    }
}
