using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


public static class TwelveDaysSong
{
    static readonly (string day, string what)[] Statements = {
        On("twelfth").Gave("twelve Drummers Drumming").Comma(),
        On("eleventh").Gave("eleven Pipers Piping").Comma(),
        On("tenth").Gave("ten Lords-a-Leaping").Comma(),
        On("ninth").Gave("nine Ladies Dancing").Comma(),
        On("eighth").Gave("eight Maids-a-Milking").Comma(),
        On("seventh").Gave("seven Swans-a-Swimming").Comma(),
        On("sixth").Gave("six Geese-a-Laying").Comma(),
        On("fifth").Gave("five Gold Rings").Comma(),
        On("fourth").Gave("four Calling Birds").Comma(),
        On("third").Gave("three French Hens").Comma(),
        On("second").Gave("two Turtle Doves").Comma().And(),
        On("first").Gave("a Partridge in a Pear Tree").Stop(),
    };

    static int VersesCount => Statements.Length;

    public static string Sing()
        => Verses(1, 12);

    public static string Verse(int verseNumber)
        => verseNumber.StatementIndices().Aggregate(
            new StringBuilder(verseNumber.BeginVerse()),
            ContinueVerse
        ).ToString() + "\n";

    public static string Verses(int start, int end)
        => string.Join("\n", Enumerable
            .Range(start, count: end - start + 1)
            .Select(Verse)
        ) + "\n";

    static string BeginVerse(this int verseNumber)
    {
        var i = verseNumber.StatementIndices().First();
        (string xth, _) = Statements[i];

        return $"On the {xth} day of Christmas my true love gave to me, ";
    }

    static StringBuilder ContinueVerse(StringBuilder buff, int i)
    {
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

    static string On(string day)
        => day;

    static (string, string) Gave(this string day, string what)
        => (day, what);

    static (string, string) And(this (string, string) x)
        => x.Suffix("and ");

    static (string, string) Comma(this (string, string) x)
        => x.Suffix(", ");

    static (string, string) Stop(this (string, string) x)
        => x.Suffix(".");

    static (string, string) Suffix(
        this (string day, string what) x,
        string suffix
    ) => (x.day, $"{x.what}{suffix}");
}