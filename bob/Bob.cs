using System;
using System.Linq;


public static class Bob
{
    static readonly (Func<string, bool> eval, string response)[] Reactions = {
        (eval: IsShouting, response: "Whoa, chill out!"),
        (eval: IsSilence,  response: "Fine. Be that way!"),
        (eval: IsQuestion, response: "Sure."),
        (eval: IsAnything, response: "Whatever.")
    };

    public static string Response(string statement)
    {
        return Reactions.First(r => r.eval(statement)).response;
    }

    static bool IsShouting(string statement)
        => statement.ToUpper() == statement
        && statement.ToLower() != statement;

    static bool IsQuestion(string statement)
        => statement.TrimEnd().EndsWith("?");

    static bool IsSilence(string statement)
        => String.IsNullOrWhiteSpace(statement);

    static bool IsAnything(string statement) => true;
}