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

    public static string Parse(string markdown) => markdown
        .Split('\n')
        .Aggregate(
            (buff: new StringBuilder(), list: false),
            (acc, line) => {
                var html = Parsers
                    .Select(parse => parse(line))
                    .First(r => null != r);

                return acc.Append(html, line.IsListItem());
            },
            acc => acc.CloseList().ToString()
        );

    static (StringBuilder buff, bool list) Append(
        this (StringBuilder buff, bool list) acc,
        string html,
        bool list
    )
    {
        if (list == acc.list)
        {
            acc.buff.Append(html);
        }
        else if (list)
        {
            acc.buff.OpenList().Append(html);
        }
        else
        {
            acc.buff.Append(html).CloseList();
        }

        return (acc.buff, list);
    }

    static StringBuilder CloseList(this (StringBuilder, bool) acc)
    {
        var (buff, list) = acc;
        return list ? buff.CloseList() : buff;
    }

    static StringBuilder CloseList(this StringBuilder buff)
        => buff.Append("</ul>");

    static StringBuilder OpenList(this StringBuilder buff)
        => buff.Append("<ul>");

    static bool IsListItem(this string text)
        => text.StartsWith("*");

    static string ParseHeader(string markdown)
    {
        var count = markdown
            .TakeWhile(c => c == '#')
            .Count();

        return count != 0
            ? markdown.Substring(count + 1).Wrap($"h{count}")
            : null;
    }

    static string ParseLineItem(string markdown)
    {
        return markdown.IsListItem()
            ? markdown.Substring(2).AsHtml().Wrap("li")
            : null;
    }

    static string ParseParagraph(string markdown)
        => markdown.AsHtml().Wrap("p");

    static string AsHtml(
        this string markdown,
        string delimiter,
        string tag
    ) => Regex.Replace(
        input:       markdown,
        pattern:     $"{delimiter}(.+){delimiter}",
        replacement: "$1".Wrap(tag)
    );

    static string AsHtml(this string markdown)
        => TagMappings.Aggregate(
            markdown,
            (text, info) => text.AsHtml(info.delimiter, info.tag)
        );

    static string Wrap(this string text, string tag)
        => $"<{tag}>{text}</{tag}>";
}