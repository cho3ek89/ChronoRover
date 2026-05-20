using ChronoRover.Models;
using ChronoRover.Helpers;
using ChronoRover.Providers.TimeZone;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Signal;

public class Dcf77SignalProvider(
    ITimeZoneProvider timeZoneProvider) : SignalProviderBase(timeZoneProvider, SignalType.Dcf77)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool[] GetMinuteSignal(DateTime dateTime)
    {
        var values = new bool[60];

        var year = dateTime.Year % 100;
        var month = dateTime.Month;
        var day = dateTime.Day;
        var min = dateTime.Minute;
        var hour = dateTime.Hour;
        // Dcf77 starts on monday (range 1-7), DayOfWeek starts on sunday (range 0-6)
        var weekday = dateTime.DayOfWeek == 0 ? 7 : (int)dateTime.DayOfWeek;
        var isDst = TimeZone.IsDaylightSavingTime(dateTime);

        // Setting values.

        // 00 - start of a minute
        // 00-15 - unused
        // 16 - ignoring summer's time begin and end dates announcement
        values[17] = isDst; // CEST ?
        values[18] = !isDst; // CET ?

        // 19 - ignoring leap second announcement

        values[20] = true; // time marker

        values[21] = Bcd1(min, 0); // min
        values[22] = Bcd1(min, 1); // min
        values[23] = Bcd1(min, 2); // min
        values[24] = Bcd1(min, 3); // min
        values[25] = Bcd10(min, 0); // min
        values[26] = Bcd10(min, 1); // min
        values[27] = Bcd10(min, 2); // min
        values[28] = Parity(values[21..28].AsSpan(), 7);

        values[29] = Bcd1(hour, 0); // hour
        values[30] = Bcd1(hour, 1); // hour
        values[31] = Bcd1(hour, 2); // hour
        values[32] = Bcd1(hour, 3); // hour
        values[33] = Bcd10(hour, 0); // hour
        values[34] = Bcd10(hour, 1); // hour
        values[35] = Parity(values[29..35].AsSpan(), 6);

        values[36] = Bcd1(day, 0); // day
        values[37] = Bcd1(day, 1); // day
        values[38] = Bcd1(day, 2); // day
        values[39] = Bcd1(day, 3); // day
        values[40] = Bcd10(day, 0); // day
        values[41] = Bcd10(day, 1); // day

        values[42] = (weekday & 1) == 1; // day of week
        values[43] = ((weekday >> 1) & 1) == 1; // day of week
        values[44] = ((weekday >> 2) & 1) == 1; // day of week

        values[45] = Bcd1(month, 0); // month
        values[46] = Bcd1(month, 1); // month
        values[47] = Bcd1(month, 2); // month
        values[48] = Bcd1(month, 3); // month
        values[49] = Bcd10(month, 0); // month

        values[50] = Bcd1(year, 0); // year
        values[51] = Bcd1(year, 1); // year
        values[52] = Bcd1(year, 2); // year
        values[53] = Bcd1(year, 3); // year
        values[54] = Bcd10(year, 0); // year
        values[55] = Bcd10(year, 1); // year
        values[56] = Bcd10(year, 2); // year
        values[57] = Bcd10(year, 3); // year

        values[58] = Parity(values[36..58].AsSpan(), 22);

        // 59 - mark

        return values;
    }

    private static bool Bcd1(int val, int pos) => BitHelper.Bcd1(val, pos);

    private static bool Bcd10(int val, int pos) => BitHelper.Bcd10(val, pos);

    private static bool Parity(ReadOnlySpan<bool> values, int length) => BitHelper.Parity(values, length);
}