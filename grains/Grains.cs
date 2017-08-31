using System;


public static class Grains
{
    public static ulong Square(int n)
        => (ulong) Math.Pow(2, n - 1);

    public static ulong Total()
        => 18_446_744_073_709_551_615L;
}
