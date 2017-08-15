using System;


public static class TwoFer
{
    public static string GetResponse(string input = null)
        => $"One for {input ?? "you"}, one for me.";
}
