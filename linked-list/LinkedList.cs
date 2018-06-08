using System;


public class Deque<T>
{
    private Node _root = new Node();

    public void Push(T value) => _root.Prev.Append(value);
    public T Pop() => _root.Prev.Remove();
    public void Unshift(T value) => _root.Append(value);
    public T Shift() => _root.Next.Remove();

    private class Node
    {
        public T Value { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }

        public Node() => this.Next = this.Prev = this;

        public void Append(T value)
        {
            this.Next = this.Next.Prev = new Node
            {
                Value = value,
                Prev  = this,
                Next  = this.Next,
            };
        }

        public T Remove()
        {
            this.Prev.Next = this.Next;
            this.Next.Prev = this.Prev;

            return this.Value;
        }
    }
}
