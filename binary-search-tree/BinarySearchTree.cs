using System;
using System.Collections;
using System.Collections.Generic;


public class BinarySearchTree
    : IEnumerable<int>
{
    public BinarySearchTree(int value)
    {
    }

    public BinarySearchTree(IEnumerable<int> values)
    {
    }

    public int Value
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public BinarySearchTree Left
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public BinarySearchTree Right
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public BinarySearchTree Add(int value)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<int> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
