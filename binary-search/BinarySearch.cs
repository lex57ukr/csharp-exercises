using System;


public class BinarySearch
{
    int[] _sourceArray;

    public int this[int index] => _sourceArray[index];

    public int Length => _sourceArray.Length;

    public BinarySearch(int[] input)
        => _sourceArray = input;

    public int Find(int value)
        => IndexOf(value, 0, this.Length - 1);

    int IndexOf(int value, int lo, int hi)
    {
        if (lo > hi)
        {
            return -1;
        }

        var i = (lo + hi) / 2;
        if (this[i] < value)
        {
            return IndexOf(value, i + 1, hi);
        }

        if (this[i] > value)
        {
            return IndexOf(value, lo, hi - 1);
        }

        return i;
    }
}
