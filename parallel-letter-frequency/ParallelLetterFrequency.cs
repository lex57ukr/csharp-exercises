using System;
using System.Linq;
using System.Collections.Generic;


public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts)
    {
        return texts
            .AsParallel()
            .SelectMany(x => x.AsEnumerable())
            .Where(char.IsLetter)
            .Select(char.ToLower)
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
    }
}
