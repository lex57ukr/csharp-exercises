using System;
using System.Collections;


public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        var acc = new ArrayList();
        Flatten(input, acc);

        return acc;
    }

    private static void Flatten(object item, ArrayList acc)
    {
        if (item == null)
        {
            return;
        }

        if (item is IEnumerable collection)
        {
            foreach (var x in collection)
            {
                Flatten(x, acc);
            }
        }
        else
        {
            acc.Add(item);
        }
    }
}
