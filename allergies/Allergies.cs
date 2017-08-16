using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class Allergies
{
    readonly (int score, string allergen)[] Allergens = {
        (score:   1, allergen: "eggs"),
        (score:   2, allergen: "peanuts"),
        (score:   4, allergen: "shellfish"),
        (score:   8, allergen: "strawberries"),
        (score:  16, allergen: "tomatoes"),
        (score:  32, allergen: "chocolate"),
        (score:  64, allergen: "pollen"),
        (score: 128, allergen: "cats"),
    };

    ImmutableList<string> _allergies;

    public Allergies(int mask)
    {
        _allergies = Allergens
            .Where(a => (mask & a.score) == a.score)
            .Select(a => a.allergen)
            .ToImmutableList();
    }

    public bool IsAllergicTo(string allergy)
        => _allergies.Contains(allergy);

    public IList<string> List()
        => _allergies;
}