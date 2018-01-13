using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;


public static class IsbnVerifier
{
    private const int ZeroChar = 48;
    private const string IsbnPattern = @"^\d(-?)\d{3}\1\d{5}\1[\dX]$";

    public static bool IsValid(string number)
        => IsIsbnLike(number) && IsbnCheckSum(number) % 11 == 0;

    private static int IsbnCheckSum(string number)
        => SequenceDigits(number)
        .Reverse()
        .Select((n, i) => n * (i + 1))
        .Sum();

    private static bool IsIsbnLike(string number)
        => Regex.IsMatch(number, IsbnPattern);

    private static IEnumerable<int> SequenceDigits(string number)
        => number.Where(x => x != '-').Select(ToInt32);

    private static int ToInt32(char c)
        => c == 'X' ? 10 : Convert.ToInt32(c) - ZeroChar;
}
