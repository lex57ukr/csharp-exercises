using System;
using System.Diagnostics;
using static System.Linq.Enumerable;


public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
        => r.Expreal(realNumber);
}

public struct RationalNumber
    : IEquatable<RationalNumber>
{
    private int _numerator, _denominator;

    public RationalNumber(int numerator, int denominator)
    {
        _numerator   = numerator;
        _denominator = numerator == 0 ? 1 : denominator;
    }

    public override string ToString()
    {
        return _numerator == _denominator
            ? _numerator.ToString()
            : $"{_numerator}/{_denominator}";
    }

    public override int GetHashCode()
    {
        var hash = 23;

        unchecked
        {
            hash = (hash * 31) + _numerator;
            hash = (hash * 31) + _denominator;
        }

        return hash;
    }

    public bool Equals(RationalNumber other)
        => _numerator == other._numerator && _denominator == other._denominator;

    public RationalNumber Add(RationalNumber r)
    {
        return new RationalNumber(
            _numerator * r._denominator + r._numerator * _denominator,
            _denominator * r._denominator
        ).Reduce();
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        => r1.Add(r2);

    public RationalNumber Sub(RationalNumber r)
    {
        return new RationalNumber(
            _numerator * r._denominator - r._numerator * _denominator,
            _denominator * r._denominator
        ).Reduce();
    }

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        => r1.Sub(r2);

    public RationalNumber Mul(RationalNumber r)
        => new RationalNumber(_numerator * r._numerator, _denominator * r._denominator).Reduce();

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        => r1.Mul(r2);

    public RationalNumber Div(RationalNumber r)
        => new RationalNumber(_numerator * r._denominator, r._numerator * _denominator).Reduce();

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        => r1.Div(r2);

    public RationalNumber Abs()
        => new RationalNumber(Math.Abs(_numerator), Math.Abs(_denominator));

    public RationalNumber Reduce()
    {
        var n = _numerator * Math.Sign(_denominator);
        var d = Math.Abs(_denominator);

        var gcb = Gcb(Math.Abs(n), d);

        return new RationalNumber(n / gcb, d / gcb);
    }

    public RationalNumber Exprational(int power)
        => new RationalNumber(Pow(_numerator, power), Pow(_denominator, power));

    public double Expreal(int baseNumber)
        => Root(Pow(baseNumber, _numerator), _denominator);

    private static int Gcb(int a, int b)
        => b == 0 ? a : Gcb(b, a % b);

    private static int Pow(int a, int b)
        => Repeat(a, b).Aggregate(1, (acc, x) => acc * x);

    private static double Root(int a, int b)
    {
        // https://en.wikipedia.org/wiki/Nth_root_algorithm
        throw new NotImplementedException();
    }
}
