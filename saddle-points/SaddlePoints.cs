using System;
using System.Linq;
using System.Collections.Generic;


public class SaddlePoints
{
    readonly int[,] _values;

    public int Width => _values.GetLength(0);

    public int Height => _values.GetLength(1);

    public SaddlePoints(int[,] values)
        => _values = values;

    public IEnumerable<Tuple<int, int>> Calculate()
        => Indices().Where(IsSaddlePoint);

    IEnumerable<Tuple<int, int>> Indices()
    {
        for (int x = 0; x < this.Width; ++x)
        {
            for (int y = 0; y < this.Height; ++y)
            {
                yield return Tuple.Create(x, y);
            }
        }
    }

    bool IsSaddlePoint(Tuple<int, int> index)
    {
        int x = index.Item1, y = index.Item2;
        int value = _values[x, y];

        return value == MaxForRow(x)
            && value == MinForColumn(y);
    }

    int MaxForRow(int row) => Enumerable
        .Range(0, this.Height)
        .Select(y => _values[row, y])
        .Max();

    int MinForColumn(int column) => Enumerable
        .Range(0, this.Width)
        .Select(x => _values[x, column])
        .Min();
}
