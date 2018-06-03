using System;
using System.Collections.Generic;


public static class ScaleGenerator
{
    public static string[] Pitches(string tonic)
        => Pitches(tonic, "mmmmmmmmmmmm");

    public static string[] Pitches(string tonic, string pattern)
    {
        var scale = UseSharps(tonic) ? Sharps : Flats;
        var start = Array.FindIndex(scale, ForCaseInsensitive(tonic));

        var pitches = new string [pattern.Length];
        for (int c = start, i = 0; i < pattern.Length; ++i)
        {
            pitches[i] = scale[c];
            c = (c + Intervals[pattern[i]]) % scale.Length;
        }

        return pitches;
    }

    private static readonly string[] Sharps = {
        "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#"
    };

    private static readonly string[] Flats = {
        "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab"
    };

    private static readonly IDictionary<char, int> Intervals
        = new Dictionary<char, int> { ['m'] = 1, ['M'] = 2, ['A'] = 3 };

    private static bool UseSharps(string tonic)
    {
        var sharps = new [] { "C", "G", "D", "A", "E", "B", "a", "e", "b" };
        return Array.IndexOf(sharps, tonic) != -1 || tonic.EndsWith("#");
    }

    private static Predicate<string> ForCaseInsensitive(string value)
        => x => StringComparer.OrdinalIgnoreCase.Equals(value, x);
}
