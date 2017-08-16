using System;


public static class CollatzConjecture
{
    public static int GetSteps(int input)
    {
        if (input <= 0)
        {
            throw new ArgumentException();
        }

        int steps = 0;
        for (var n = input; n != 1; n = Next(n))
        {
            steps++;
        }

        return steps;
    }

    static int Next(int n) => n % 2 == 0
        ? n / 2
        : 3 * n + 1;
}
