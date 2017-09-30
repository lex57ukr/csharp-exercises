using System;
using System.Linq;
using System.Collections.Generic;


public class Anagram
{
    private string BaseWord { get; }
    private string Alphabet { get; }
    private static IEqualityComparer<string> Comparer
        => StringComparer.OrdinalIgnoreCase;

    public Anagram(string baseWord)
    {
        this.BaseWord = baseWord;
        this.Alphabet = GetAlphabet(baseWord);
    }

    public string[] Anagrams(string[] potentialMatches)
        => potentialMatches
            .Distinct(Comparer)
            .Where(IsAnagram)
            .ToArray();

    private static string GetAlphabet(string word)
        => string.Concat(word.Select(char.ToLower).OrderBy(c => c));

    private bool IsAnagram(string word)
    {
        return ! Comparer.Equals(this.BaseWord, word)
            && this.Alphabet == GetAlphabet(word);
    }
}
