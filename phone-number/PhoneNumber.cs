using System;
using System.Linq;
using System.Collections.Generic;


public class PhoneNumber
{
    const int LenNanpLong = 11;
    const int LenNanpShort = 10;
    const string Skip = "\x20\t+-.()";

    public static string Clean(string phoneNumber)
    {
        var digits = string.Concat(
            EnumDigitsOrThrow(phoneNumber)
        );

        var nanpShortNumber = ToNanpShortNumber(digits);
        if (string.IsNullOrEmpty(nanpShortNumber))
        {
            throw new ArgumentException(
                $"{phoneNumber} is not a valid phone number."
            );
        }

        return nanpShortNumber;
    }

    static IEnumerable<char> EnumDigitsOrThrow(string text) => text
        .Select(FilterDigitOrThrow)
        .Where(x => x.keep)
        .Take(LenNanpLong + 1)
        .Select(x => x.value);

    static (char value, bool keep) FilterDigitOrThrow(char c)
    {
        if (char.IsDigit(c))
        {
            return (value: c, keep: true);
        }

        if (Skip.Contains(c))
        {
            return (value: c, keep: false);
        }

        throw new ArgumentException($"Unexpected char '{c}'.");
    }

    static string ToNanpShortNumber(string digits)
    {
        switch (digits.Length)
        {
            case LenNanpLong:
                if (IsNanpCountry(digits))
                {
                    return ToNanpShortNumber(digits.Substring(1));
                }
                break;

            case LenNanpShort:
                if (IsNanpShort(digits))
                {
                    return digits;
                }
                break;
        }

        return string.Empty;
    }

    static bool IsNanpCountry(string phoneNumber)
        => phoneNumber[0] == '1';

    static bool IsNanpShort(string phoneNumber)
    {
        var parts = Parts(phoneNumber);
        return IsNanpCode(parts.area) && IsNanpCode(parts.exchange);
    }

    static bool IsNanpCode(string part)
        => part[0] >= '2' && part[0] <= '9';

    static (string area, string exchange, string subscriber) Parts(
        string phoneNumber
    ) => (
        area:       phoneNumber.Substring(0, length: 3),
        exchange:   phoneNumber.Substring(3, length: 3),
        subscriber: phoneNumber.Substring(6)
    );
}
