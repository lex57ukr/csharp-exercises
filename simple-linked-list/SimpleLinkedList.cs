using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class SimpleLinkedList<T>
    : IEnumerable<T>
{
    public SimpleLinkedList(T value)
        => this.Value = value;

    public SimpleLinkedList(IEnumerable<T> values)
        : this(values.First())
    {
        AddMany(values.Skip(1));
    }

    public T Value
    {
        get;
        private set;
    }

    public SimpleLinkedList<T> Next
    {
        get;
        private set;
    }

    public SimpleLinkedList<T> Add(T value)
    {
        if (this.Next == null)
        {
            this.Next = new SimpleLinkedList<T>(value);
        }
        else
        {
            this.Next.Add(value);
        }

        return this;
    }

    public SimpleLinkedList<T> AddMany(IEnumerable<T> values)
    {
        foreach (var value in values.Reverse())
        {
            this.Next = new SimpleLinkedList<T>(value)
            {
                Next = this.Next
            };
        }

        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var curr = this; curr != null; curr = curr.Next)
        {
            yield return curr.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
