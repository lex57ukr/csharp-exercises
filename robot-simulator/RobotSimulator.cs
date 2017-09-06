using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


public enum Bearing
{
    North,
    East,
    South,
    West
}


public struct Coordinate
{
    public Coordinate(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X
    {
        get;
    }

    public int Y
    {
        get;
    }

    public static Coordinate operator + (Coordinate a, Coordinate b)
        => new Coordinate(a.X + b.X, a.Y + b.Y);
}


public class RobotSimulator
{
    static readonly IDictionary<Bearing, Coordinate> Deltas
        = new Dictionary<Bearing, Coordinate> {
            [Bearing.North] = new Coordinate(x: 0, y: 1),
            [Bearing.East]  = new Coordinate(x: 1, y: 0),
            [Bearing.South] = new Coordinate(x: 0, y: -1),
            [Bearing.West]  = new Coordinate(x: -1, y: 0),
        }.ToImmutableDictionary();

    readonly Queue<Bearing> _bearings = new Queue<Bearing>(
        Enum.GetValues(typeof(Bearing)).Cast<Bearing>()
    );

    readonly IDictionary<char, Action> _simInstrHandlers;

    public RobotSimulator(Bearing bearing, Coordinate coordinate)
    {
        this.Bearing    = bearing;
        this.Coordinate = coordinate;

        _simInstrHandlers = new Dictionary<char, Action> {
            ['L'] = this.TurnLeft,
            ['R'] = this.TurnRight,
            ['A'] = this.Advance,
        }.ToImmutableDictionary();
    }

    public Bearing Bearing
    {
        get => _bearings.Peek();
        private set
        {
            while (value != this.Bearing)
            {
                TurnRight();
            }
        }
    }

    public Coordinate Coordinate
    {
        get;
        private set;
    }

    public void TurnRight()
        => _bearings.Enqueue(_bearings.Dequeue());

    public void TurnLeft()
    {
        for (var i = _bearings.Count - 1; i > 0; --i)
        {
            TurnRight();
        }
    }

    public void Advance()
        => this.Coordinate += Deltas[this.Bearing];

    public void Simulate(string instructions)
    {
        foreach (char code in instructions)
        {
            _simInstrHandlers[code]();
        }
    }
}