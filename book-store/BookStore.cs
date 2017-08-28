using System;
using System.Linq;
using System.Collections.Immutable;
using System.Collections.Generic;


public static class BookStore
{
    const double PricePerBook = 8.0;
    static readonly (int books, double discount)[] Discounts = {
        (books: 2, discount: 0.05),
        (books: 3, discount: 0.10),
        (books: 4, discount: 0.20),
        (books: 5, discount: 0.25),
    };

    static int MinDiscountedGroupSize => Discounts
        .Select(d => d.books)
        .First();

    public static double Total(IEnumerable<int> books)
    {
        var stacks = books
            .GroupBy(x => x)
            .Select(ImmutableQueue.CreateRange<int>)
            .ToImmutableArray();

        if (stacks.IsEmpty)
        {
            return 0;
        }

        return Enumerable
            .Range(MinDiscountedGroupSize, stacks.Length)
            .AsParallel()
            .Select(count => stacks.GroupBooks(count))
            .Select(PriceOfGroups)
            .Min();
    }

    static IEnumerable<IEnumerable<int>> GroupBooks(
        this IEnumerable<ImmutableQueue<int>> stacks,
        int maxBooksPerGroup
    ) => stacks.GroupBooks(
        maxBooksPerGroup,
        ImmutableList<IEnumerable<int>>.Empty
    );

    static IEnumerable<IEnumerable<int>> GroupBooks(
        this IEnumerable<ImmutableQueue<int>> stacks,
        int maxBooksPerGroup,
        ImmutableList<IEnumerable<int>> groups
    )
    {
        var res = stacks
            .Take(maxBooksPerGroup)
            .Aggregate(
                (
                    group: ImmutableList<int>.Empty,
                    head:  ImmutableList<ImmutableQueue<int>>.Empty
                ),
                (acc, stack) => {
                    var group = acc.group.Add(stack.Peek());
                    var rem   = stack.Dequeue();
                    return (
                        group: group,
                        head: (rem.IsEmpty ? acc.head : acc.head.Add(rem))
                    );
                }
            );

        if (res.group.IsEmpty)
        {
            return groups;
        }

        return GroupBooks(
            res.head.Concat(stacks.Skip(maxBooksPerGroup)),
            maxBooksPerGroup,
            groups.Add(res.group)
        );
    }

    static double Discount(int countOfBooks) => Discounts
        .Where(d => d.books == countOfBooks)
        .Select(d => d.discount)
        .FirstOrDefault();

    static double PriceOfGroup(IEnumerable<int> books)
    {
        var countOfBooks = books.Count();

        var fullPrice = PricePerBook * countOfBooks;
        var discount  = fullPrice * Discount(countOfBooks);

        return fullPrice - discount;
    }

    static double PriceOfGroups(IEnumerable<IEnumerable<int>> groups)
        => groups.Select(PriceOfGroup).Sum();
}
