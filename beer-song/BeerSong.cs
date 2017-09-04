using System;
using System.Linq;
using System.Collections.Generic;


public static class BeerSong
{
    public static string Verse(int number)
        => Verses(number, number);

    public static string Verses(int begin, int end)
        => string.Join("\n", AllLyrics(begin, end)) + "";

    static IEnumerable<string> AllLyrics(int begin, int end)
        => Enumerable
            .Range(0, count: begin - end + 1)
            .Select(i => begin - i)
            .Select(Lyrics);

    static string Lyrics(int number)
    {
        switch (number)
        {
            case 0:
                return Verse0();

            case 1:
                return Verse1();

            case 2:
                return Verse2();

            default:
                return VerseN(number);
        }
    }

    static string Verse0()
    {
        return "No more bottles of beer on the wall, no more bottles of beer.\n"
            + "Go to the store and buy some more, 99 bottles of beer on the wall.\n";
    }

    static string Verse1()
    {
        return "1 bottle of beer on the wall, 1 bottle of beer.\n"
            + "Take it down and pass it around, no more bottles of beer on the wall.\n";
    }

    static string Verse2()
    {
        return "2 bottles of beer on the wall, 2 bottles of beer.\n"
            + "Take one down and pass it around, 1 bottle of beer on the wall.\n";
    }

    static string VerseN(int n)
    {
        return $"{n} bottles of beer on the wall, {n} bottles of beer.\n"
            + $"Take one down and pass it around, {n - 1} bottles of beer on the wall.\n";
    }
}