using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;


public static class CryptoSquare
{
    public static string Ciphertext(string plaintext)
    {
        if (plaintext == string.Empty)
        {
            return plaintext;
        }

        var normalText   = ToNormalForm(plaintext);
        var (rows, cols) = GetDimensions(normalText.Length);

        return normalText
            .Select((c, i) => (Char: c, Index: i))
            .GroupBy(x => x.Index / cols, x => x.Char)
            .Aggregate(new char [rows, cols], ComposeRow, ToCipherForm);
    }

    private static (int rows, int cols) GetDimensions(int length)
    {
        var cols = (int) Math.Ceiling(Math.Sqrt(length));
        var rows = (int) Math.Ceiling((double) length / cols);

        return (rows, cols);
    }

    private static string ToNormalForm(string text)
        => Regex.Replace(text, @"([^\w]|\s)+", "").ToLower();

    private static string ToCipherForm(char[,] acc)
    {
        var lines = Range(0, acc.GetLength(1)).Select(
            c => Range(0, acc.GetLength(0)).Select(r => acc[r, c])
        ).Select(string.Concat<char>);

        return string.Join(" ", lines);
    }

    private static char[,] ComposeRow(char[,] acc, IGrouping<int, char> g)
    {
        var col     = 0;
        var padding = Repeat(' ', acc.GetLength(1) - g.Count());

        foreach (var c in g.Concat(padding))
        {
            acc[g.Key, col++] = c;
        }

        return acc;
    }
}
