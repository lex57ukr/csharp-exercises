using System;
using System.Linq;
using System.Collections.Generic;


public static class Acronym
{
    static readonly char[] Separators = {' ', '-', ','};

    public static string Abbreviate(string phrase)
        => string.Concat(phrase.TitleLetters());

    static IEnumerable<char> TitleLetters(this string phrase) => phrase
        .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
        .Select(w => w[0])
        .Select(char.ToUpper);
}
