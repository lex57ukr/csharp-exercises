using System;
using System.Linq;
using System.Collections.Generic;


public class SaddlePoints
{
    readonly int[,] _values;
    readonly Lazy<int[]> _maxInRows;
    readonly Lazy<int[]> _minInColumns;

    int Width => _values.GetLength(0);
    int Height => _values.GetLength(1);

    public SaddlePoints(int[,] values)
    {
        _values = values;

        _maxInRows = new Lazy<int[]>(
            () => Enumerable
                .Range(0, this.Width)
                .Select(MaxForRow)
                .ToArray(),
            isThreadSafe: true
        );

        _minInColumns = new Lazy<int[]>(
            () => Enumerable
                .Range(0, this.Height)
                .Select(MinForColumn)
                .ToArray(),
            isThreadSafe: true
        );
    }

    public IEnumerable<Tuple<int, int>> Calculate()
        => Indices().AsParallel().Where(IsSaddlePoint);

    IEnumerable<Tuple<int, int>> Indices() =>
        from x in Enumerable.Range(0, this.Width)
        from y in Enumerable.Range(0, this.Height)
        select Tuple.Create(x, y);

    bool IsSaddlePoint(Tuple<int, int> index)
    {
        int x = index.Item1, y = index.Item2;
        int value = _values[x, y];

        return IsMaxForRow(value, x)
            && IsMinForColumn(value, y);
    }

    bool IsMaxForRow(int value, int row)
        => value == _maxInRows.Value[row];

    bool IsMinForColumn(int value, int column)
        => value == _minInColumns.Value[column];

    int MaxForRow(int row) => Enumerable
        .Range(0, this.Height)
        .Select(y => _values[row, y])
        .Max();

    int MinForColumn(int column) => Enumerable
        .Range(0, this.Width)
        .Select(x => _values[x, column])
        .Min();
}
