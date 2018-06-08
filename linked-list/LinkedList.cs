using System;


public class Deque<T>
{
    private Node _head, _tail;

    public void Push(T value)
        => _tail = Add(new Node(value, head: _tail), ref _head);

    public T Pop() => Remove(_tail, _tail.UnlinkPrev, ref _tail);

    public void Unshift(T value)
        => _head = Add(new Node(value, tail: _head), ref _tail);

    public T Shift() => Remove(_head, _head.UnlinkNext, ref _head);

    private static Node Add(Node node, ref Node root)
    {
        if (root == null)
        {
            root = node;
        }

        return node;
    }

    private static T Remove(Node node, Func<Node> f, ref Node root)
    {
        root = f();
        return node.Value;
    }

    internal class Node
    {
        public T Value { get; }
        private Node Prev { get; set; }
        private Node Next { get; set; }

        public Node (T value, Node head = null, Node tail = null)
        {
            this.Value = value;

            this.Prev = head;
            if (head != null)
            {
                head.Next = this;
            }

            this.Next = tail;
            if (tail != null)
            {
                tail.Prev = this;
            }
        }

        public Node UnlinkPrev()
        {
            if (this.Prev != null)
            {
                this.Prev.Next = null;
            }

            return this.Prev;
        }

        public Node UnlinkNext()
        {
            if (this.Next != null)
            {
                this.Next.Prev = null;
            }

            return this.Next;
        }
    }
}
