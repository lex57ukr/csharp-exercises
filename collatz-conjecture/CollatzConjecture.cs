using System;


public static class CollatzConjecture
{
    public static int GetSteps(int input)
    {
        if (input <= 0)
        {
            throw new ArgumentException();
        }

        return CountSteps(input, step: 0);
    }

    static int CountSteps(int n, int step)
    {
        if (1 == n)
        {
            return step;
        }

        return CountSteps(Next(n), step + 1);
    }

    static int Next(int n) => n % 2 == 0
        ? n / 2
        : 3 * n + 1;
}
