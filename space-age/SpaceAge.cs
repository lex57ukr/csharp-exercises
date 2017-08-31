using System;


public class SpaceAge
{
    const int EarthYearSeconds = 31_557_600;

    readonly long Seconds;

    public SpaceAge(long seconds)
        => this.Seconds = seconds;

    public double OnEarth()
        => EarthYears();

    public double OnMercury()
        => EarthYears(orbitalPeriod: 0.2408467);

    public double OnVenus()
        => EarthYears(orbitalPeriod: 0.61519726);

    public double OnMars()
        => EarthYears(orbitalPeriod: 1.8808158);

    public double OnJupiter()
        => EarthYears(orbitalPeriod: 11.862615);

    public double OnSaturn()
        => EarthYears(orbitalPeriod: 29.447498);

    public double OnUranus()
        => EarthYears(orbitalPeriod: 84.016846);

    public double OnNeptune()
        => EarthYears(orbitalPeriod: 164.79132);

    double EarthYears(double orbitalPeriod = 1) => Math.Round(
        this.Seconds / SecondsPerEarthYear(orbitalPeriod),
        digits: 2
    );

    static double SecondsPerEarthYear(double orbitalPeriod)
        => orbitalPeriod * EarthYearSeconds;
}
