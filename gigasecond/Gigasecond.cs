using System;


public static class Gigasecond
{
    const long Seconds = 1_000_000_000L;
    const long Ticks   = Seconds * TimeSpan.TicksPerSecond;

    public static DateTime Add(DateTime birthDate)
        => birthDate.AddTicks(Ticks);
}
