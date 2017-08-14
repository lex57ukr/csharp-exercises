using System;
using System.Linq;
using System.Collections.Generic;

public class DNA
{
    public DNA(string sequence)
    {
        foreach (char c in sequence)
        {
            if (this.NucleotideCounts.ContainsKey(c))
            {
                this.NucleotideCounts[c] += 1;
            }
        }
    }

    public IDictionary<char, int> NucleotideCounts
    {
        get;
    } = new Dictionary<char, int> {
        {'A', 0},
        {'T', 0},
        {'C', 0},
        {'G', 0}
    };

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
