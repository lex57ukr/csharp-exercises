using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Immutable;
using static System.Linq.Enumerable;



public static class Grep
{
    public static string Find(string pattern, string flags, string[] files)
    {
        var options        = ParseFlags(flags);
        var predicate      = AsPredicate(pattern, options);
        var resultSelector = ResultSelectorFrom(options, files.Length > 1);

        var results = files
            .Select(x => FindInFile(x, predicate, resultSelector))
            .ComposeResults(options)
            .ToImmutableList();

        return results.IsEmpty
            ? string.Empty
            : results.Add(string.Empty).Join("\n");
    }

    [Flags]
    private enum Options
    {
        None = 0,
        PrintLineNumbers = 1,
        PrintFileNamesOnly = 2,
        IgnoreCase = 4,
        Inverted = 8,
        MatchWholeLineOnly = 16,
    }

    private static readonly IDictionary<string, Options> FlagsToOptions
        = new Dictionary<string, Options>
        {
            ["n"] = Options.PrintLineNumbers,
            ["l"] = Options.PrintFileNamesOnly,
            ["i"] = Options.IgnoreCase,
            ["v"] = Options.Inverted,
            ["x"] = Options.MatchWholeLineOnly,
        }.ToImmutableDictionary();

    private delegate string ResultSelector(string fileName, int lineNumber, string text);

    private static Options ParseFlags(string flags)
    {
        var delimiters = new [] {' ', '-'};

        return flags.Split(delimiters).Aggregate(
            Options.None,
            (acc, x) => FlagsToOptions.TryGetValue(x, out var opts)
                ? acc | opts
                : acc
        );
    }

    private static Predicate<string> AsPredicate(string pattern, Options options)
        => AsPredicate(AsRegex(pattern, options), options);

    private static Predicate<string> AsPredicate(Regex pattern, Options options)
    {
        return options.Enabled(Options.Inverted)
            ? Not(pattern.IsMatch)
            : pattern.IsMatch;
    }

    private static Regex AsRegex(string pattern, Options options)
    {
        return new Regex(
            AsRegexPattern(pattern, options),
            AsRegexOptions(options) | RegexOptions.Compiled
        );
    }

    private static RegexOptions AsRegexOptions(Options options)
    {
        return options.Enabled(Options.IgnoreCase)
            ? RegexOptions.IgnoreCase
            : RegexOptions.None;
    }

    private static string AsRegexPattern(string patters, Options options)
    {
        var escapedPattern = Regex.Escape(patters);

        return options.Enabled(Options.MatchWholeLineOnly)
            ? $"^{escapedPattern}$"
            : escapedPattern;
    }

    private static ResultSelector ResultSelectorFrom(Options options, bool manyFiles)
    {
        if (options.Enabled(Options.PrintFileNamesOnly))
        {
            return PrintFileName;
        }

        var selectors = ImmutableList<ResultSelector>.Empty;

        if (manyFiles)
        {
            selectors = selectors.Add(PrintFileName);
        }

        if (options.Enabled(Options.PrintLineNumbers))
        {
            selectors = selectors.Add(PrintLineNumber);
        }

        if (selectors.IsEmpty)
        {
            return PrintText;
        }

        selectors = selectors.Add(PrintText);

        return (fileName, lineNumber, text) => selectors
            .Select(f => f(fileName, lineNumber, text))
            .Join(":");
    }

    private static string PrintFileName(string fileName, int lineNumber, string text)
        => fileName;

    private static string PrintLineNumber(string fileName, int lineNumber, string text)
        => lineNumber.ToString();

    private static string PrintText(string fileName, int lineNumber, string text)
        => text;

    private static IEnumerable<string> FindInFile(
        string filePath,
        Predicate<string> predicate,
        ResultSelector result
    )
    {
        var fileName = Path.GetFileName(filePath);

        return File.ReadLines(filePath)
            .Select((text, i) => new { text, lineNumber = i + 1 })
            .Where(x => predicate(x.text))
            .Select(x => result(fileName, x.lineNumber, x.text));
    }

    private static IEnumerable<string> ComposeResults(
        this IEnumerable<IEnumerable<string>> results,
        Options options
    )
    {
        if (! options.Enabled(Options.PrintFileNamesOnly))
        {
            return results.SelectMany(x => x);
        }

        return results.Select(x => x.FirstOrDefault()).Where(x => x != null);
    }

    private static bool Enabled(this Options options, Options mask)
        => (options & mask) == mask;

    private static Predicate<string> Not(Predicate<string> predicate)
        => x => ! predicate(x);

    private static string Join<T>(this IEnumerable<T> source, string delimiter)
        => string.Join(delimiter, source);
}
