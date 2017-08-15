using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


using Plants = System.Collections.Generic.IEnumerable<Plant>;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}


public class Garden
{
    const int CupsPerPerson = 2;

    static readonly IImmutableDictionary<char, Plant> Plants
        = new Dictionary<char, Plant> {
            ['V'] = Plant.Violets,
            ['R'] = Plant.Radishes,
            ['C'] = Plant.Clover,
            ['G'] = Plant.Grass,
        }.ToImmutableDictionary();

    ILookup<string, Plants> ChildToPlantedCups
    {
        get;
    }

    public Garden(IEnumerable<string> children, string windowSills)
    {
        IEnumerable<(string child, Plants plants)> AssignToChildren(
            IEnumerable<Plants> plantedCups
        ) => children
            .OrderBy(c => c)
            .Zip(plantedCups, (c, p) => (child: c, plants: p));

        this.ChildToPlantedCups = windowSills
            .Split('\n')
            .Select(AsPlants)
            .Select(AsPlantedCups)
            .SelectMany(AssignToChildren)
            .ToLookup(x => x.child, x => x.plants);
    }

    public Plants GetPlants(string child)
        => this.ChildToPlantedCups[child].SelectMany(p => p);

    public static Garden DefaultGarden(string windowSills)
    {
        var children = new [] {
            "Alice", "Bob", "Charlie", "David",
            "Eve", "Fred", "Ginny", "Harriet",
            "Ileana", "Joseph", "Kincaid", "Larry"
        };

        return new Garden(children, windowSills);
    }

    static Plants AsPlants(string windowSill)
        => windowSill.Select(c => Plants[c]);

    static IEnumerable<Plants> AsPlantedCups(Plants plants)
        => plants.BatchMap(size: CupsPerPerson, mapper: x => x);
}

static class EnumerableExtensions
{
    public static IEnumerable<TResult> BatchMap<TItem, TResult>(
        this IEnumerable<TItem> source,
        int size,
        Func<IEnumerable<TItem>, TResult> mapper
    ) => source
        .Select((item, i) => (item: item, index: i))
        .GroupBy(x => x.index / size)
        .Select(g => mapper(g.Select(x => x.item)));
}
