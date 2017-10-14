using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;


public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        ThrowIfInvalid(inputBase, inputDigits, outputBase);

        var number = Compose(inputDigits, inputBase);
        return ToBase(number, outputBase);
    }

    private static int Compose(int[] digits, int @base)
    {
        int Weigh(int digit, int index)
            => digit * (int) Pow(@base, index);

        return digits.Reverse().Select(Weigh).Sum();
    }

    private static int[] ToBase(int number, int @base)
    {
        int Weigh(int index)
            => (number / (int) Pow(@base, index)) % @base;

        var length = Length(number, @base);
        return Enumerable.Range(0, length).Select(Weigh).Reverse().ToArray();
    }

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
        => digits
        .TakeWhile(x => x == 0)
        .Select(x => true)
        .FirstOrDefault();

    private static bool HasInvalidDigit(int @base, int[] digits)
        => digits
        .Where(x => x >= @base || x < 0)
        .Select(x => true)
        .FirstOrDefault();
}
