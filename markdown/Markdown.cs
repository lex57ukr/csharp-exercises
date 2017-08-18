using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;


public static class Markdown
{
    static readonly (string delimiter, string tag)[] TextMarkup = {
        (delimiter: "__", tag: "strong"),
        (delimiter: "_",  tag: "em"),
    };

    static readonly Func<string, bool, (string html, bool list)>[] Parsers = {
        ParseHeader,
        ParseLineItem,
        ParseParagraph
    };

    private static string Wrap(this string text, string tag)
        => $"<{tag}>{text}</{tag}>";

    private static string WrapIf(this string text, string tag, bool cond)
        => cond ? text.Wrap(tag) : text;

    private static string Parse(
        this string markdown,
        string delimiter,
        string tag
    ) => Regex.Replace(
        input:       markdown,
        pattern:     $"{delimiter}(.+){delimiter}",
        replacement: "$1".Wrap(tag)
    );

    private static string ParseText(this string markdown, bool list)
        => TextMarkup.Aggregate(
            markdown,
            (text, info) => text.Parse(info.delimiter, info.tag)
        ).WrapIf("p", ! list);

    private static (string html, bool list) ParseHeader(
        string markdown,
        bool list
    )
    {
        var count = markdown
            .TakeWhile(c => c == '#')
            .Count();

        if (count == 0)
        {
            return (null, list);
        }

        var headerHtml = markdown
            .Substring(count + 1)
            .Wrap($"h{count}");

        return (
            list ? $"</ul>{headerHtml}" : headerHtml,
            false
        );
    }

    private static (string html, bool list) ParseLineItem(
        string markdown,
        bool list
    )
    {
        if ( ! markdown.StartsWith("*"))
        {
            return (null, list);
        }

        var innerHtml = markdown
            .Substring(2)
            .ParseText(list: true)
            .Wrap("li");

        return (
            list ? innerHtml : $"<ul>{innerHtml}",
            true
        );
    }

    private static (string html, bool list) ParseParagraph(
        string markdown,
        bool list
    )
    {
        var html = markdown.ParseText(list);
        return (
            list ? $"</ul>{html}" : html,
            false
        );
    }

    private static (string html, bool line) ParseLine(
        this string markdown,
        bool list
    ) => Parsers.Select(p => p(markdown, list))
                .FirstOrDefault(r => r.html != null);

    public static string Parse(string markdown)
    {
        var lines  = markdown.Split('\n');
        var result = new StringBuilder();
        var list   = false;

        for (int i = 0; i < lines.Length; i++)
        {
            var (html, newList) = ParseLine(lines[i], list);
            if (html == null)
            {
                throw new ArgumentException("Invalid markdown");
            }

            result.Append(html);
            list = newList;
        }

        if (list)
        {
            result.Append("</ul>");
        }

        return result.ToString();
    }
}