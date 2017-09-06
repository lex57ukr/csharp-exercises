using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

public static class SecretHandshake
{
    const string Reverse = ":reverse";

    static readonly (int mask, string code)[] Codes = {
        (mask: 1, code:  "wink"),
        (mask: 2, code:  "double blink"),
        (mask: 4, code:  "close your eyes"),
        (mask: 8, code:  "jump"),
        (mask: 16, code: Reverse),
    };

    public static string[] Commands(int commandValue) => Codes
        .Aggregate(
            ImmutableList<string>.Empty,
            (acc, ctl) => (ctl.mask & commandValue) == ctl.mask
                ? acc.Apply(ctl.code)
                : acc
        ).ToArray();

    static ImmutableList<string> Apply(
        this ImmutableList<string> acc,
        string code
    ) => code == Reverse ? acc.Reverse() : acc.Add(code);
}
