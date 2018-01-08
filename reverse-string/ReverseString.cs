using System;


public static class ReverseString
{
    public static string Reverse(string input)
    {
        var chars = input.ToCharArray();
        for (int i = 0, count = chars.Length / 2; i < count; ++i)
        {
            Swap(chars, i, chars.Length - i - 1);
        }

        return new string(chars);
    }

    private static void Swap<T>(T[] array, int i, int j)
    {
        var t = array[i];
        array[i] = array[j];
        array[j] = t;
    }
}
