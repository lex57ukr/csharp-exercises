using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


public static class House
{
    static readonly (string, string)[] Statements = new [] {
        The("horse and the hound and the horn").That("belonged").To(),
        The("farmer sowing his corn").That("kept"),
        The("rooster that crowed in the morn").That("woke"),
        The("priest all shaven and shorn").That("married"),
        The("man all tattered and torn").That("kissed"),
        The("maiden all forlorn").That("milked"),
        The("cow with the crumpled horn").That("tossed"),
        The("dog").That("worried"),
        The("cat").That("killed"),
        The("rat").That("ate"),
        The("malt").That("lay").In(),
        The("house that Jack built").Stop(),
    };

    static int VersesCount => Statements.Length;

    public static string Verse(int number)
        => number.StatementIndices().Aggregate(
            new StringBuilder("This is"),
            ContinueVerse
        ).ToString();

    public static string Verses(int first, int last)
        => string.Join("\n\n", Enumerable
            .Range(first, count: last - first + 1)
            .Select(Verse)
        );

    static StringBuilder ContinueVerse(StringBuilder buff, int i)
    {
        buff.Append(Statements[i].Item1);
        buff.Append(Statements[i].Item2);

        return buff;
    }

    static IEnumerable<int> StatementIndices(this int verse)
        => Enumerable.Range(
            verse.ToStatementIndex(),
            verse.LinesCount()
        );

    static int ToStatementIndex(this int verseNumber)
        => VersesCount - verseNumber;

    static int LinesCount(this int verseNumber)
        => VersesCount - verseNumber.ToStatementIndex();

    static string The(string subject)
        => $" the {subject}";

    static (string, string) That(this string subject, string verb)
        => (subject, $"\nthat {verb}");

    static (string, string) To(this (string, string) x)
        => x.Add("to");

    static (string, string) In(this (string, string) x)
        => x.Add("in");

    static (string, string) Stop(this string subject)
        => ($"{subject}.", string.Empty);

    static (string, string) Add(
        this (string subject, string verb) x,
        string suffix
    ) => (x.subject, $"{x.verb} {suffix}");
}