using System;
using System.Collections.Generic;


public class Queen
{
    public int Row { get; }
    public int Column { get; }

    public Queen(int row, int column)
    {
        this.Row    = row;
        this.Column = column;
    }
}


public static class Queens
{
    public static bool CanAttack(Queen white, Queen black)
    {
        (int x, int y) = ProjectAxis(white, black);

        bool IsDiagonal() => x == y;
        bool IsSameRow() => x == 0;
        bool IsSameColumn() => y == 0;

        if (IsSameRow() && IsSameColumn())
        {
            throw new ArgumentException();
        }

        return IsDiagonal() || IsSameRow() || IsSameColumn();
    }

    static (int x, int y) ProjectAxis(Queen a, Queen b) => (
        x: Math.Abs(a.Row - b.Row),
        y: Math.Abs(a.Column - b.Column)
    );
}
