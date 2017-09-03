using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public static class ProteinTranslation
{
    const string Stop = "STOP";
    const int CodonGroupLength = 3;

    static readonly IDictionary<string, string> Proteins
        = new Dictionary<string, string[]>
        {
            ["Cysteine"]      = new [] {"UGU", "UGC"},
            ["Leucine"]       = new [] {"UUA", "UUG"},
            ["Methionine"]    = new [] {"AUG"},
            ["Phenylalanine"] = new [] {"UUU", "UUC"},
            ["Serine"]        = new [] {"UCU", "UCC", "UCA", "UCG"},
            ["Tryptophan"]    = new [] {"UGG"},
            ["Tyrosine"]      = new [] {"UAU", "UAC"},
            [Stop]            = new [] {"UAA", "UAG", "UGA"},
        }.Unpack<string, string, string[]>();

    public static string[] Translate(string codon)
    {
        try
        {
            return codon
                .BatchSelect(CodonGroupLength, string.Concat)
                .Select(Protein)
                .TakeWhile(Continue)
                .ToArray();
        }
        catch (KeyNotFoundException)
        {
            throw new Exception("Bad RNA");
        }
    }

    static string Protein(string codon)
        => Proteins[codon];

    static bool Continue(string codon)
        => codon != Stop;
}


static class EnumerableExtensions
{
    public static IEnumerable<TResult> BatchSelect<TItem, TResult>(
        this IEnumerable<TItem> source,
        int size,
        Func<IEnumerable<TItem>, TResult> mapper
    ) => source
        .Select((item, i) => (item: item, index: i))
        .GroupBy(x => x.index / size)
        .Select(g => mapper(g.Select(x => x.item)));
}


static class DictionaryExtensions
{
    public static IDictionary<TKey, TValue> Unpack<TKey, TValue, TKeys>(
        this IDictionary<TValue, TKeys> source
    ) where TKeys: IEnumerable<TKey>
    {
        return source.Aggregate(
            ImmutableDictionary<TKey, TValue>.Empty,
            (acc, kvp) => kvp.Value.Aggregate(
                acc,
                (a, c) => a.Add(c, kvp.Key)
            )
        );
    }
}
