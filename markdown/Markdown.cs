using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;


public static class Markdown
{
    static readonly (string delimiter, string tag)[] TagMappings = {
        (delimiter: "__", tag: "strong"),
        (delimiter: "_",  tag: "em"),
    };

    static readonly Func<string, string>[] Parsers = {
        ParseHeader,
        ParseLineItem,
        ParseParagraph
    };

    private static bool IsListItem(this string text)
        => text.StartsWith("*");

    private static string OpenList(this string html)
        => "<ul>" + html;

    private static string CloseList(this string html)
        => html + "</ul>";

    private static string Wrap(this string text, string tag)
        => $"<{tag}>{text}</{tag}>";

    private static string AsHtml(
        this string markdown,
        string delimiter,
        string tag
    ) => Regex.Replace(
        input:       markdown,
        pattern:     $"{delimiter}(.+){delimiter}",
        replacement: "$1".Wrap(tag)
    );

    private static string AsHtml(this string markdown)
        => TagMappings.Aggregate(
            markdown,
            (text, info) => text.AsHtml(info.delimiter, info.tag)
        );

    private static string ParseHeader(string markdown)
    {
        var count = markdown
            .TakeWhile(c => c == '#')
            .Count();

        if (count == 0)
        {
            return null;
        }

        return markdown
            .Substring(count + 1)
            .Wrap($"h{count}");
    }

    private static string ParseLineItem(string markdown)
    {
        if ( ! markdown.IsListItem())
        {
            return null;
        }

        return markdown
            .Substring(2)
            .AsHtml()
            .Wrap("li");
    }

    private static string ParseParagraph(string markdown)
        => markdown.AsHtml().Wrap("p");

    private static (string html, bool list) Parse(
        this string markdown,
        bool list
    )
    {
        var html = Parsers
            .Select(parse => parse(markdown))
            .First(r => null != r);

        var newList = markdown.IsListItem();

        if (list == newList)
        {
            return (html, list);
        }

        if (newList)
        {
            return (html.OpenList(), list: true);
        }

        return (html.CloseList(), list: false);
    }

    public static string Parse(string markdown) => markdown
        .Split('\n')
        .Aggregate(
            (buff: new StringBuilder(), list: false),
            (acc, line) => {
                var (html, list) = line.Parse(acc.list);
                acc.buff.Append(html);
                return (acc.buff, list);
            }
        ).FinalizeHtml();

    private static string FinalizeHtml(this (StringBuilder, bool) acc)
    {
        var (buff, list) = acc;
        return list
            ? buff.ToString().CloseList()
            : buff.ToString();
    }
}