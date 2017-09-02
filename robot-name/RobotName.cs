using System;
using System.Linq;
using System.Collections.Generic;


public class Robot
{
    static class NameFactory
    {
        const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const int PrefixLength = 2;
        const int SeriesCount = 1000;

        static readonly object Monitor = new object();
        static readonly Random Rand = new Random();
        static readonly HashSet<string> ExistingNames = new HashSet<string>();

        static string Prefix => string.Concat(
            Enumerable
                .Range(0, count: PrefixLength)
                .Select(_ => Rand.Next(maxValue: Alpha.Length))
                .Select(i => Alpha[i])
        );

        static int Series => Rand.Next(maxValue: SeriesCount);

        static string Format(string prefix, int series)
            => $"{prefix}{series:000}";

        static IEnumerable<string> RandomNames()
        {
            while (true)
            {
                yield return Format(Prefix, Series);
            }
        }

        public static string New()
        {
            lock (Monitor)
            {
                var name = RandomNames()
                    .SkipWhile(ExistingNames.Contains)
                    .First();

                ExistingNames.Add(name);
                return name;
            }
        }
    }

    public string Name
    {
        get;
        private set;
    } = NameFactory.New();

    public void Reset()
    {
        this.Name = NameFactory.New();
    }
}
