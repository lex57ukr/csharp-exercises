using System;
using System.Linq;
using System.Collections.Generic;


public static class Isogram
{
    public static bool IsIsogram(string word) => word
        .Where(char.IsLetter)
        .Select(char.ToLower)
        .GroupBy(c => c)
        .FirstOrDefault(g => g.Count() > 1) == null;
}
