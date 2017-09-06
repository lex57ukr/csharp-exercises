using System;
using System.Linq;
using System.Collections.Generic;


public static class SecretHandshake
{
    const int CtlReverse = 16;

    static readonly (int mask, string code)[] Codes = {
        (mask: 1, code:  "wink"),
        (mask: 2, code:  "double blink"),
        (mask: 4, code:  "close your eyes"),
        (mask: 8, code:  "jump"),
    };

    public static string[] Commands(int commandValue)
    {
        bool IsOn(int mask)
            => (mask & commandValue) == mask;

        return Codes
            .Where(ctl => IsOn(ctl.mask))
            .Select(ctl => ctl.code)
            .ReverseIfTrue(IsOn(CtlReverse))
            .ToArray();
    }

    static IEnumerable<string> ReverseIfTrue(
        this IEnumerable<string> source,
        bool cond
    ) => cond ? source.Reverse() : source;
}
