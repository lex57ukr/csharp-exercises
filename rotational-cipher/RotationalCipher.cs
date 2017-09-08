using System;
using System.Linq;
using System.Collections.Generic;


public static class RotationalCipher
{
    public static string Rotate(string text, int shift)
        => string.Concat(text.Rot(shift));

    static IEnumerable<char> Rot(this string text, int shift)
        => text.Select(c => c.Rot(shift));

    static char Rot(this char c, int shift)
    {
        if ('A' <= c && c <= 'Z')
        {
            return c.Rot('A', shift);
        }

        if ('a' <= c && c <= 'z')
        {
            return c.Rot('a', shift);
        }

        return c;
    }

    static char Rot(this char x, char a, int shift)
        => Rot((int) x, (int) a, shift);

    static char Rot(int x, int a, int shift)
        => (char) (a + (x - a + shift) % 26);
}

