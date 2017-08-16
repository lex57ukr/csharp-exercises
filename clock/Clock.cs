using System;
using System.Collections.Generic;


public class Clock
    : IEquatable<Clock>
{
    const int HoursPerDay    = 24;
    const int MinutesPerHour = 60;
    const int MinutesPerDay  = HoursPerDay * MinutesPerHour;

    public int Hours   => this.TotalMinutes / MinutesPerHour;

    public int Minutes => this.TotalMinutes % MinutesPerHour;

    public int TotalMinutes
    {
        get;
    }

    Clock(int totalMinutes)
    {
        int CappedMinutes()
            => totalMinutes % MinutesPerDay;

        int WrappedMinutes()
            => MinutesPerDay - Math.Abs(CappedMinutes());

        this.TotalMinutes = totalMinutes > 0
            ? CappedMinutes()
            : WrappedMinutes();
    }

    public Clock(int hours, int minutes = 0)
        : this(MinutesPerHour * hours + minutes)
    {
    }

    public Clock Add(int minutesToAdd)
    {
        return new Clock(this.TotalMinutes + minutesToAdd);
    }

    public Clock Subtract(int minutesToSubtract)
    {
        return new Clock(this.TotalMinutes - minutesToSubtract);
    }

    public override string ToString()
        => $"{this.Hours:00}:{this.Minutes:00}";

    public bool Equals(Clock other)
        => null != other && this.TotalMinutes == other.TotalMinutes;

    public override int GetHashCode() => this.TotalMinutes;
}
