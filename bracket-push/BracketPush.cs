using System;
using System.Linq;
using System.Collections.Generic;


public static class BracketPush
{
    public static bool IsPaired(string input)
    {
        var track = new Stack<char>();
        return input.All(track.IsBalanced) && track.Count == 0;
    }

    private static readonly (char open, char close)[] BracketPairs =
    {
        ('[', ']'), ('{', '}'), ('(', ')')
    };

    private static bool IsBalanced(this Stack<char> track, char c)
    {
        var pair = Array.Find(BracketPairs, x => c == x.open || c == x.close);

        if (pair.open == c)
        {
            track.Push(pair.close);
        }
        else if (pair.close == c)
        {
            return track.TryPop(out var expected) && expected == c;
        }

        return true;
    }
}
