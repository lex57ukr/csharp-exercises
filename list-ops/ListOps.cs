using System;
using System.Collections.Generic;


public static class ListOps
{
    public static int Length<T>(List<T> input)
        => input.Count;

    public static List<T> Reverse<T>(List<T> input)
        => Foldr((x, acc) => Add(acc, x), new List<T>(), input);

    public static List<TOut> Map<TIn, TOut>(Func<TIn, TOut> map, List<TIn> input)
        => Foldl((acc, x) => Add(acc, map(x)), new List<TOut>(), input);

    public static List<T> Filter<T>(Func<T, bool> predicate, List<T> input)
        => Foldl((acc, x) => AddIf(predicate, acc, x), new List<T>(), input);

    public static TOut Foldl<TIn, TOut>(Func<TOut, TIn, TOut> func, TOut start, List<TIn> input)
        => Fold(func, start, input);

    public static TOut Foldr<TIn, TOut>(Func<TIn, TOut, TOut> func, TOut start, List<TIn> input)
        => Fold((acc, x) => func(x, acc), start, Reversed(input));

    public static List<T> Concat<T>(List<List<T>> input)
        => Foldl(Append, new List<T>(), input);

    public static List<T> Append<T>(List<T> left, List<T> right)
        => Foldl(Add, new List<T>(left), right);

    private static TOut Fold<TIn, TOut>(Func<TOut, TIn, TOut> func, TOut start, IEnumerable<TIn> input)
    {
        var acc = start;
        foreach (var x in input)
        {
            acc = func(acc, x);
        }

        return acc;
    }

    private static IEnumerable<T> Reversed<T>(List<T> list)
    {
        for (var i = Length(list) - 1; i >= 0; --i)
        {
            yield return list[i];
        }
    }

    private static List<T> Add<T>(List<T> acc, T x)
    {
        acc.Add(x);
        return acc;
    }

    private static List<T> AddIf<T>(Func<T, bool> predicate, List<T> acc, T x)
        => predicate(x) ? Add(acc, x) : acc;
}
