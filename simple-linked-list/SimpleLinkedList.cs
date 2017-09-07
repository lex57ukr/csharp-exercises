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
        foreach (var value in values.Skip(1))
        {
            Add(value);
        }
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
