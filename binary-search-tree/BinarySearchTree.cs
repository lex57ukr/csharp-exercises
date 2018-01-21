using System;
using System.Collections;
using System.Collections.Generic;
using static System.Linq.Enumerable;


public class BinarySearchTree
    : IEnumerable<int>
{
    private readonly int _value;
    private BinarySearchTree _left, _right;

    public int Value => _value;
    public BinarySearchTree Left => _left;
    public BinarySearchTree Right => _right;

    public BinarySearchTree(int value)
        => _value = value;

    public BinarySearchTree(IEnumerable<int> values)
    {
        using (var enumerator = values.GetEnumerator())
        {
            if (! enumerator.MoveNext())
            {
                throw new ArgumentException("The collection is empty.");
            }

            _value = enumerator.Current;
            while (enumerator.MoveNext())
            {
                Add(enumerator.Current);
            }
        }
    }

    public BinarySearchTree Add(int value)
    {
        if (value <= this.Value)
        {
            _left = Add(value, _left);
        }
        else
        {
            _right = Add(value, _right);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return Enumerate(this.Left)
            .Concat(Repeat(this.Value, 1))
            .Concat(Enumerate(this.Right))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    private static IEnumerable<int> Enumerate(BinarySearchTree tree)
    {
        if (tree == null)
        {
            yield break;
        }

        foreach (var x in tree)
        {
            yield return x;
        }
    }

    private static BinarySearchTree Add(int value, BinarySearchTree tree)
        => tree?.Add(value) ?? new BinarySearchTree(value);
}
