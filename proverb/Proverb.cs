using System;
using System.Linq;
using System.Collections.Generic;


public static class Proverb
{
    static readonly string[] Lines = {
        "For want of a nail the shoe was lost.",
        "For want of a shoe the horse was lost.",
        "For want of a horse the rider was lost.",
        "For want of a rider the message was lost.",
        "For want of a message the battle was lost.",
        "For want of a battle the kingdom was lost.",
        "And all for the want of a horseshoe nail.",
    };

    public static string Line(int line)
        => Lines[line - 1];

    public static string AllLines()
        => String.Join("\n", Lines);
}
