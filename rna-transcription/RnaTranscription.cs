using System;
using System.Linq;
using System.Collections.Generic;


public static class RnaTranscription
{
    public static string ToRna(string nucleotide) => nucleotide
        .Select(AsRnaNucleotide)
        .AsString();

    static char AsRnaNucleotide(char dna)
    {
        switch (dna)
        {
            case 'G': return 'C';
            case 'C': return 'G';
            case 'T': return 'A';
            case 'A': return 'U';

            default:
                throw new ArgumentException($"Invalid {dna} nucleotide.");
        }
    }

    static string AsString(this IEnumerable<char> chars)
        => new string(chars.ToArray());
}
