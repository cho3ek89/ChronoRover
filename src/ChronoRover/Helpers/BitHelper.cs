using System;

namespace ChronoRover.Helpers;

public static class BitHelper
{
    public static bool Bcd1(int val, int pos)
    {
        val %= 10;
        return ((val >> pos) & 1) == 1;
    }

    public static bool Bcd10(int val, int pos)
    {
        val /= 10;
        return Bcd1(val, pos);
    }

    public static bool Bcd100(int val, int pos)
    {
        val /= 100;
        return Bcd1(val, pos);
    }

    public static bool Parity(ReadOnlySpan<bool> values, int length)
    {
        var isEven = true;

        for (var i = 0; i < length; i++)
        {
            if (values[i])
            {
                isEven = !isEven;
            }
        }

        return !isEven;
    }
}