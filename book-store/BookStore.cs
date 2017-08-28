using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public static class BookStore
{
    const double PricePerBook = 8.0;
    const int MinDiscountedGroupSize = 2;
    static readonly (int groupSize, double discount)[] Discounts = {
        (groupSize: 2, discount: 0.05),
        (groupSize: 3, discount: 0.10),
        (groupSize: 4, discount: 0.20),
        (groupSize: 5, discount: 0.25),
    };

    public static double Total(IEnumerable<int> books)
    {
        var stacks = books
            .GroupBy(x => x)
            .Select(ImmutableQueue.CreateRange<int>)
            .ToImmutableArray();

        IEnumerable<int> DiscountedGroupSizes() => Enumerable.Range(
            start: MinDiscountedGroupSize,
            count: Math.Max(stacks.Length - 1, 1)
        );

        return DiscountedGroupSizes()
            .Select(groupSize => stacks.GroupByTitles(groupSize))
            .Select(Total)
            .DefaultIfEmpty(0)
            .Min();
    }

    static double Total(IEnumerable<IEnumerable<int>> groups) => groups
        .Select(g => g.Count())
        .Select(Total)
        .Sum();

    static double Total(int groupSize) =>
        PricePerBook * groupSize * (1 - Discount(groupSize));

    static double Discount(int groupSize) => Discounts
        .Where(d => d.groupSize == groupSize)
        .Select(d => d.discount)
        .FirstOrDefault();

    static IEnumerable<IEnumerable<int>> GroupByTitles(
        this IEnumerable<ImmutableQueue<int>> stacks,
        int groupSize
    ) => stacks.GroupByTitles(
        groupSize,
        ImmutableList<IEnumerable<int>>.Empty
    );

    static IEnumerable<IEnumerable<int>> GroupByTitles(
        this IEnumerable<ImmutableQueue<int>> stacks,
        int groupSize,
        ImmutableList<IEnumerable<int>> groups
    )
    {
        var (group, tail) = stacks.Slice(groupSize);
        return group.IsEmpty
            ? groups
            : tail.GroupByTitles(groupSize, groups.Add(group));
    }

    static (ImmutableList<int>, IEnumerable<ImmutableQueue<int>>) Slice(
        this IEnumerable<ImmutableQueue<int>> stacks,
        int groupSize
    ) => stacks
        .Take(groupSize)
        .Aggregate(
            (
                group: ImmutableList<int>.Empty,
                tail:  stacks.Skip(groupSize).ToImmutableList()
            ),
            (acc, stack) => {
                var group = acc.group.Add(stack.Peek());
                var rem   = stack.Dequeue();
                return (
                    group: group,
                    tail:  (rem.IsEmpty ? acc.tail : acc.tail.Add(rem))
                );
            }
        );
}
