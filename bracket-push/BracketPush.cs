using System;
using System.Collections.Generic;


public static class BracketPush
{
    public static bool IsPaired(string input)
    {
        var track = new Stack<char>();
        foreach (char c in input)
        {
            if (! IsBalanced(c, track))
            {
                return false;
            }
        }

        return track.Count == 0;
    }

    private static readonly (char open, char close)[] BracketPairs =
    {
        ('[', ']'), ('{', '}'), ('(', ')')
    };

    private static bool IsBalanced(char c, Stack<char> track)
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
