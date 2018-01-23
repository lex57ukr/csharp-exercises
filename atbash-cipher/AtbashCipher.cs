using System;
using System.Linq;


public static class AtbashCipher
{
    private const int GroupSize = 5;
    private const int AlphabetSize = 26;

    public static string Encode(string plainValue)
    {
        var transcoded = plainValue
            .Where(char.IsLetterOrDigit)
            .Select(char.ToLower)
            .Select(Transcode);

        var groups = transcoded.Select(
            (x, i) => (Char: x, Index: i)
        ).GroupBy(
            x => x.Index / GroupSize,
            x => x.Char
        ).Select(
            x => string.Concat(x)
        );

        return string.Join(" ", groups);
    }

    public static string Decode(string encodedValue)
    {
        var transcoded = encodedValue
            .Where(char.IsLetterOrDigit)
            .Select(Transcode);

        return string.Concat(transcoded);
    }

    private static char Transcode(char c)
        => char.IsLetter(c) ? Encode(c) : c;

    private static char Encode(char c)
        => (char) (Encode((int) c - (int) 'a') + (int) 'a');

    private static int Encode(int x)
        => (AlphabetSize - 1) * (x + 1) % AlphabetSize;
}
