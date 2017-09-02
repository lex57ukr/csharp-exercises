using System;
using System.Linq;
using System.Collections.Generic;


public static class Proverb
{
    static readonly Func<string>[] Lines = {
        "nail".Caused("shoe").ToBe().Lost(),
        "shoe".Caused("horse").ToBe().Lost(),
        "horse".Caused("rider").ToBe().Lost(),
        "rider".Caused("message").ToBe().Lost(),
        "message".Caused("battle").ToBe().Lost(),
        "battle".Caused("kingdom").ToBe().Lost(),
        "And all for the want of a horseshoe nail.".ToBe(),
    };

    public static IEnumerable<int> Indices
        => Enumerable.Range(1, Lines.Length);

    public static string Line(int line)
        => Lines[line - 1]();

    public static string AllLines()
        => String.Join("\n", Indices.Select(Line));

    static (string, string) Caused(
        this string subject, string item
    ) => (subject, item);

    static Func<T> ToBe<T>(this T value)
        => (() => value);

    static Func<string> Lost(this Func<(string, string)> context)
        => (() => context().Lament());

    static string Lament(this (string subject, string item) x)
        => $"For want of a {x.subject} the {x.item} was lost.";
}
