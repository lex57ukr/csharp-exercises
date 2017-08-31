using System;


public class SpaceAge
{
    const int EarthYearSeconds = 31_557_600;

    static class OrbitalPeriod
    {
        public const double Mercury = 0.2408467;
        public const double Venus   = 0.61519726;
        public const double Mars    = 1.8808158;
        public const double Jupiter = 11.862615;
        public const double Saturn  = 29.447498;
        public const double Uranus  = 84.016846;
        public const double Neptune = 164.79132;
    }

    readonly long Seconds;

    public SpaceAge(long seconds)
        => this.Seconds = seconds;

    public double OnEarth()
        => EarthYears();

    public double OnMercury()
        => EarthYears(OrbitalPeriod.Mercury);

    public double OnVenus()
        => EarthYears(OrbitalPeriod.Venus);

    public double OnMars()
        => EarthYears(OrbitalPeriod.Mars);

    public double OnJupiter()
        => EarthYears(OrbitalPeriod.Jupiter);

    public double OnSaturn()
        => EarthYears(OrbitalPeriod.Saturn);

    public double OnUranus()
        => EarthYears(OrbitalPeriod.Uranus);

    public double OnNeptune()
        => EarthYears(OrbitalPeriod.Neptune);

    double EarthYears(double orbitalPeriod = 1) => Math.Round(
        this.Seconds / SecondsPerEarthYear(orbitalPeriod),
        digits: 2
    );

    static double SecondsPerEarthYear(double orbitalPeriod)
        => orbitalPeriod * EarthYearSeconds;
}
