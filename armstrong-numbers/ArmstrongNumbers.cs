using System;
using static System.Linq.Enumerable;


public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var digits = CountDigits(number);

        return number == Range(0, digits)
            .Select(i => Digit(number, i))
            .Select(n => Pow(n, digits))
            .Sum();
    }

    private static int CountDigits(int number)
        => (int) Math.Log10(number) + 1;

    private static int Digit(int number, int index)
        => (number / Pow(10, index)) % 10;

    private static int Pow(int x, int y)
        => (int) Math.Pow(x, y);
}
