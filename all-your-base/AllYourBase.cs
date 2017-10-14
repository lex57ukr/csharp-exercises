using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Linq.Enumerable;


public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        ThrowIfInvalid(inputBase, inputDigits, outputBase);

        var number = Compose(inputDigits, inputBase);
        return Convert(number, outputBase);
    }

    private static int Compose(int[] digits, int @base)
    {
        int Weigh(int digit, int index)
            => digit * Pow(@base, index);

        return digits.Reverse().Select(Weigh).Sum();
    }

    private static int[] Convert(int number, int @base)
    {
        int Digit(int index)
            => (number / Pow(@base, index)) % @base;

        var length = Length(number, @base);
        return Range(0, length).Select(Digit).Reverse().ToArray();
    }

    private static int Pow(int x, int y)
        => (int) Math.Pow(x, y);

    private static int Length(int number, int @base)
        => (int) (Log(number) / Log(@base)) + 1;

    private static void ThrowIfInvalid(int inputBase, int[] inputDigits, int outputBase)
    {
        if ( ! IsValidBase(inputBase))
        {
            throw new ArgumentException(
                "Invalid input base.",
                paramName: nameof(inputBase)
            );
        }

        if (inputDigits.Length == 0)
        {
            throw new ArgumentException(
                "Empty number.",
                paramName: nameof(inputDigits)
            );
        }

        if (HasLeadingZero(inputDigits))
        {
            throw new ArgumentException(
                "No leading zero(s).",
                paramName: nameof(inputDigits)
            );
        }

        if (HasInvalidDigit(inputBase, inputDigits))
        {
            throw new ArgumentException(
                "Invalid number.",
                paramName: nameof(inputDigits)
            );
        }

        if ( ! IsValidBase(outputBase))
        {
            throw new ArgumentException(
                "Invalid output base.",
                paramName: nameof(outputBase)
            );
        }
    }

    private static bool IsValidBase(int @base)
        => @base > 1;

    private static bool HasLeadingZero(int[] digits)
        => digits.TakeWhile(x => x == 0).IsNotEmpty();

    private static bool HasInvalidDigit(int @base, int[] digits)
        => digits.Where(x => x >= @base || x < 0).IsNotEmpty();

    private static bool IsNotEmpty(this IEnumerable<int> digits)
        => digits.Select(_ => true).FirstOrDefault();
}
