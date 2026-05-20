using ChronoRover.Models;
using ChronoRover.Helpers;
using ChronoRover.Providers.TimeZone;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Signal;

public class BpcSignalProvider(
    ITimeZoneProvider timeZoneProvider) : SignalProviderBase(timeZoneProvider, SignalType.Bpc)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool[] GetMinuteSignal(DateTime dateTime)
    {
        var hour = dateTime.Hour % 12;
        var min = dateTime.Minute;
        // BPC starts on monday (range 1-7), DayOfWeek starts on sunday (range 0-6)
        var weekday = dateTime.DayOfWeek == 0 ? 7 : (int)dateTime.DayOfWeek;
        var isPm = dateTime.Hour >= 12;
        var day = dateTime.Day;
        var month = dateTime.Month;
        var year = dateTime.Year % 100;

        var values = new bool[120];

        values[02] = false; // sec (00)
        values[03] = false; // sec (00)
        FillWithBinary(values.AsSpan(6, 4), hour);
        FillWithBinary(values.AsSpan(10, 6), min);
        FillWithBinary(values.AsSpan(17, 3), weekday);
        values[20] = isPm;
        values[21] = Parity(values.AsSpan(2, 18), 18);
        FillWithBinary(values.AsSpan(23, 5), day);
        FillWithBinary(values.AsSpan(28, 4), month);
        FillWithBinary(values.AsSpan(32, 7), year);
        ShiftOneLeft(values.AsSpan(32, 7));
        values[39] = Parity(values.AsSpan(22, 16), 16);

        // Copying over first 20 seconds. 
        values.AsSpan(0, 40).CopyTo(values.AsSpan(40, 40));
        values[42] = false; // sec (20)
        values[43] = true; // sec (20)
        values[61] = Parity(values.AsSpan(42, 18), 18); // needs a recalc - it depends on a second 

        // Copying over first 20 seconds.
        values.AsSpan(0, 40).CopyTo(values.AsSpan(80, 40));
        values[82] = true; // sec (40)
        values[83] = false; // sec (40)
        values[101] = Parity(values.AsSpan(82, 18), 18); // needs a recalc - it depends on a second

        return values;
    }

    private static void FillWithBinary(Span<bool> bits, int value)
    {
        for (var i = 0; i < bits.Length; i++)
        {
            var pos = bits.Length - 1 - i;
            bits[i] = (value & (1 << pos)) != 0;
        }
    }

    private static void ShiftOneLeft(Span<bool> bits)
    {
        var first = bits[0];

        for (var i = 0; i < bits.Length - 1; i++)
        {
            bits[i] = bits[i + 1];
        }

        bits[^1] = first;
    }

    private static bool Parity(ReadOnlySpan<bool> values, int length) => BitHelper.Parity(values, length);
}