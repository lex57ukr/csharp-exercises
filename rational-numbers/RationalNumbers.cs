using System;
using System.Diagnostics;


public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
    {
        throw new NotImplementedException("You need to implement this extension method.");
    }
}

public struct RationalNumber
    : IEquatable<RationalNumber>
{
    private int _numerator, _denominator;

    public RationalNumber(int numerator, int denominator)
    {
        _numerator   = numerator;
        _denominator = denominator;
    }

    public RationalNumber Add(RationalNumber r)
    {
        return new RationalNumber(
            _numerator * r._denominator + r._numerator * _denominator,
            _denominator * r._denominator
        );
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
    {
        return _numerator   == other._numerator
            && _denominator == other._denominator;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        => r1.Add(r2);

    public RationalNumber Sub(RationalNumber r)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
    {
        throw new NotImplementedException("You need to implement this operator.");
    }

    public RationalNumber Mul(RationalNumber r)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
    {
        throw new NotImplementedException("You need to implement this operator.");
    }

    public RationalNumber Div(RationalNumber r)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
    {
        throw new NotImplementedException("You need to implement this operator.");
    }

    public RationalNumber Abs()
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public RationalNumber Reduce()
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public RationalNumber Exprational(int power)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public double Expreal(int baseNumber)
    {
        throw new NotImplementedException("You need to implement this function.");
    }
}
