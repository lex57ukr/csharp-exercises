﻿using System;
using System.Linq;
using System.Collections.Generic;


public static class Pangram
{
    const int AlphabetSize = 26;

    public static bool IsPangram(string input) => input
        .ToLowerInvariant()
        .Distinct()
        .Count(IsLetter) == AlphabetSize;

    static bool IsLetter(this char c)
        => c >= 'a' && c <= 'z';
}
