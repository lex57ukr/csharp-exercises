using System;
using System.Collections;
using System.Collections.Generic;


public class BinarySearchTree
    : IEnumerable<int>
{
    public int Value
    {
        get;
    }

    public BinarySearchTree Left
    {
        get;
        private set;
    }

    public BinarySearchTree Right
    {
        get;
        private set;
    }

    public BinarySearchTree(int value)
        => this.Value = value;

    public BinarySearchTree(IEnumerable<int> values)
    {
        using (var enumerator = values.GetEnumerator())
        {
            if (! enumerator.MoveNext())
            {
                throw new ArgumentException("The collection is empty.");
            }

            this.Value = enumerator.Current;
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
            AddLeft(value);
        }
        else
        {
            AddRight(value);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        if (this.Left != null)
        {
            foreach (var x in this.Left)
            {
                yield return x;
            }
        }

        yield return this.Value;

        if (this.Right != null)
        {
            foreach (var x in this.Right)
            {
                yield return x;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    private void AddLeft(int value)
    {
        if (this.Left == null)
        {
            this.Left = new BinarySearchTree(value);
        }
        else
        {
            this.Left.Add(value);
        }
    }

    private void AddRight(int value)
    {
        if (this.Right == null)
        {
            this.Right = new BinarySearchTree(value);
        }
        else
        {
            this.Right.Add(value);
        }
    }
}
