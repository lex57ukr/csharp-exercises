using System.Collections;


public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        foreach (var x in input)
        {
            if (x == null)
            {
                continue;
            }

            if (x is IEnumerable collection)
            {
                foreach (var n in Flatten(collection))
                {
                    yield return n;
                }
            }
            else
            {
                yield return x;
            }
        }
    }
}
