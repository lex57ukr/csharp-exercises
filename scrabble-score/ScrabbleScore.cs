using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public static class ScrabbleScore
{
    static readonly IDictionary<char, int> Scores = new Dictionary<int, string> {
        [1]  = "AEIOULNRST",
        [2]  = "DG",
        [3]  = "BCMP",
        [4]  = "FHVWY",
        [5]  = "K",
        [8]  = "JX",
        [10] = "QZ",
    }.UnpackScores();

    public static int Score(string input) => input
        .Select(char.ToUpperInvariant)
        .Select(c => Scores[c])
        .Sum();

    private static IDictionary<char, int> UnpackScores(
        this IDictionary<int, string> packedScores
    ) => packedScores.Aggregate(
        ImmutableDictionary<char, int>.Empty,
        (acc, kvp) => kvp.Value.Aggregate(
            acc, (a, c) => a.Add(c, kvp.Key)
        )
    );
}
