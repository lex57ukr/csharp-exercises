using System;


public static class ReverseString
{
    public static string Reverse(string input)
    {
        var chars = input.ToCharArray();
        Reverse(chars);

        return new string(chars);
    }

    private static void Reverse<T>(T[] array)
    {
        for (int i = 0, count = array.Length / 2; i < count; ++i)
        {
            Swap(array, i, chars.Length - i - 1);
        }
    }

    private static void Swap<T>(T[] array, int i, int j)
    {
        var t = array[i];
        array[i] = array[j];
        array[j] = t;
    }
}
