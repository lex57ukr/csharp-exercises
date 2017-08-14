using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public class DNA
{
    const string Strand = "ATCG";

    public DNA(string sequence)
    {
        KeyValuePair<char, int> IndexedCount(char nucleotide)
            => new KeyValuePair<char, int>(
                nucleotide,
                sequence.Count(c => c == nucleotide)
            );

        this.NucleotideCounts = Strand
            .AsParallel()
            .Select(IndexedCount)
            .ToImmutableDictionary();
    }

    public IDictionary<char, int> NucleotideCounts
    {
        get;
    }

    public int Count(char nucleotide)
    {
        int count;
        if ( ! this.NucleotideCounts.TryGetValue(nucleotide, out count))
        {
            throw new InvalidNucleotideException();
        }

        return count;
    }
}

public class InvalidNucleotideException : Exception { }
