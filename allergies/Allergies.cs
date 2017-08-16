using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class Allergies
{
    readonly (int score, string name)[] Allergens = {
        (score:   1, name: "eggs"),
        (score:   2, name: "peanuts"),
        (score:   4, name: "shellfish"),
        (score:   8, name: "strawberries"),
        (score:  16, name: "tomatoes"),
        (score:  32, name: "chocolate"),
        (score:  64, name: "pollen"),
        (score: 128, name: "cats"),
    };

    ImmutableList<string> _allergies;

    public Allergies(int mask)
    {
        _allergies = Allergens
            .Where(a => (mask & a.score) == a.score)
            .Select(a => a.name)
            .ToImmutableList();
    }

    public bool IsAllergicTo(string allergy)
        => _allergies.Contains(allergy);

    public IList<string> List()
        => _allergies;
}