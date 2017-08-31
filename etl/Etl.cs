using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public static class ETL
{
    public static IDictionary<string, int> Transform(
        IDictionary<int, IList<string>> old
    ) => old.Aggregate(
        ImmutableDictionary<string, int>.Empty,
        (acc, kvp) => kvp.Value.Aggregate(
            acc,
            (agg, c) => agg.Add(c.ToLower(), kvp.Key)
        )
    );
}
