using System;
using System.Linq;
using System.Collections.Generic;


public static class RotationalCipher
{
    static readonly (
        Func<char, bool> eval,
        Func<char, int, char> rot
    )[] Rotators = {
        (eval: IsUpper, rot: (c, shift) => c.Rot('A', shift)),
        (eval: IsLower, rot: (c, shift) => c.Rot('a', shift)),
    };

    public static string Rotate(string text, int shift)
        => string.Concat(text.Rot(shift));

    static IEnumerable<char> Rot(this string text, int shift)
        => text.Select(c => c.Rot(shift));

    static char Rot(this char c, int shift)
        => Rotators
        .Where(x => x.eval(c))
        .Select(x => x.rot(c, shift))
        .DefaultIfEmpty(c)
        .First();

    static char Rot(this char x, char a, int shift)
        => Rot((int) x, (int) a, shift);

    static char Rot(int x, int a, int shift)
        => (char) (a + (x - a + shift) % 26);

    static bool IsUpper(this char c)
        => 'A' <= c && c <= 'Z';

    static bool IsLower(this char c)
        => 'a' <= c && c <= 'z';
}

