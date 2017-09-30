using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class WordCount
{
    private static readonly char[] Delimiters = {
        ' ', ',', '\n'
    };

    private static readonly Regex RxWordExtractor =
        new Regex(@"^\W*(\w+)\W*$");

    public static IDictionary<string, int> Countwords(string phrase)
        => Words(phrase)
            .GroupBy(w => w)
            .ToDictionary(
                keySelector: g => g.Key,
                elementSelector: g => g.Count()
            );

    private static IEnumerable<string> Words(string phrase)
        => phrase
            .Split(Delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(UnwrapNormalizedWord);

    private static string UnwrapNormalizedWord(string input)
        => RxWordExtractor
            .Replace(input, replacement: "$1")
            .ToLower();
}