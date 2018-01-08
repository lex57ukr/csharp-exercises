using System;
using System.Text;
using System.Linq;


public static class RomanNumeralExtension
{
    private static readonly (int Arabic, string Roman)[]
        Numerals = new [] {
            (1000, "M"),
            (900,  "CM"),
            (500,  "D"),
            (400,  "CD"),
            (100,  "C"),
            (90,   "XC"),
            (50,   "L"),
            (40,   "XL"),
            (10,   "X"),
            (9,    "IX"),
            (5,    "V"),
            (4,    "IV"),
            (1,    "I"),
        };

    public static string ToRoman(this int value)
    {
        var rem = value;
        var acc = new StringBuilder();

        while (rem > 0)
        {
            var num = Numerals.First(x => x.Arabic <= rem);

            rem -= num.Arabic;
            acc.Append(num.Roman);
        }

        return acc.ToString();
    }
}
