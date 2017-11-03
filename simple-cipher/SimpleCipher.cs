using System;
using System.Linq;
using System.Collections.Generic;
using static System.String;
using static System.Linq.Enumerable;


public class Cipher
{
    private const int MinRandomKeyLength = 100;
    private const string Alpha = "abcdefghijklmnopqrstuvwxyz";
    private static readonly Random Rand = new Random();

    private string _key;

    public string Key
    {
        get => _key;
        private set
        {
            if ( ! IsValidKey(value))
            {
                throw new ArgumentException("Invalid key.");
            }

            _key = value;
        }
    }

    public Cipher(string key)
        => Key = key;

    public Cipher()
        => _key = CreateRandomKey();

    public string Encode(string plaintext)
        => Transform(plaintext, EncodeDistance);

    public string Decode(string ciphertext)
        => Transform(ciphertext, DecodeDistance);

    private string Transform(string text, Func<char, char, int> distance)
        => Concat(
            Range(0, text.Length)
                .Select(i => distance(text[i], this.Key[i]))
                .Select(d => Alpha[d % Alpha.Length])
        );

    private static int EncodeDistance(char c, char k)
        => Alpha.IndexOf(c) + Alpha.IndexOf(k);

    private static int DecodeDistance(char c, char k)
        => Alpha.IndexOf(c) - Alpha.IndexOf(k) + Alpha.Length;

    private static string CreateRandomKey()
        => Concat(
            Range(0, MinRandomKeyLength)
                .Select(i => Rand.Next(Alpha.Length))
                .Select(i => Alpha[i])
        );

    private static bool IsValidKey(string key)
        => ! IsNullOrEmpty(key) && key.All(c => Alpha.Contains(c));
}
