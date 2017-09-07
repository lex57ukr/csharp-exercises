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
        this.Tail().Next = new SimpleLinkedList<T>(value);
        return this;
    }

    public SimpleLinkedList<T> AddMany(IEnumerable<T> values)
    {
        this.Tail().Next = Link(values);
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

    SimpleLinkedList<T> Tail()
        => this.Next == null ? this : this.Next.Tail();

    static SimpleLinkedList<T> Link(IEnumerable<T> values)
        => values
        .Reverse()
        .Aggregate(
            default(SimpleLinkedList<T>),
            (head, v) => new SimpleLinkedList<T>(v) {Next = head}
        );
}
