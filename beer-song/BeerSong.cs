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
            .Select(LyricsFactory.New);

    static class LyricsFactory
    {
        public static string New(int number)
        {
            switch (number)
            {
                case 0:
                    return Verse0();

                case 1:
                    return VerseN(
                        now:     "1 bottle",
                        left:    "no more bottles",
                        subject: "it"
                    );

                case 2:
                    return VerseN(
                        now:  "2 bottles",
                        left: "1 bottle"
                    );

                default:
                    return VerseN(
                        now:  $"{number} bottles",
                        left: $"{number - 1} bottles"
                    );
            }
        }

        static string Verse0()
            => "No more bottles of beer on the wall, no more bottles of beer.\n"
            + "Go to the store and buy some more, 99 bottles of beer on the wall.\n";

        static string VerseN(string now, string left, string subject = "one")
            => $"{now} of beer on the wall, {now} of beer.\n"
            + $"Take {subject} down and pass it around, {left} of beer on the wall.\n";
    }
}