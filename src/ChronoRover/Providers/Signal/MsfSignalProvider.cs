using ChronoRover.Models;
using ChronoRover.Helpers;
using ChronoRover.Providers.TimeZone;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Signal;

public class MsfSignalProvider(
    ITimeZoneProvider timeZoneProvider) : SignalProviderBase(timeZoneProvider, SignalType.Msf)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool[] GetMinuteSignal(DateTime dateTime)
    {
        var year = dateTime.Year % 100;
        var month = dateTime.Month;
        var day = dateTime.Day;
        var weekday = (int)dateTime.DayOfWeek;
        var hour = dateTime.Hour;
        var min = dateTime.Minute;
        var isDst = TimeZone.IsDaylightSavingTime(dateTime);

        var values = new bool[60];

        // Setting values.

        // 00 - start of a minute
        // 01-16 - DUT1, ignoring

        values[17] = Bcd10(year, 3); // year
        values[18] = Bcd10(year, 2); // year
        values[19] = Bcd10(year, 1); // year
        values[20] = Bcd10(year, 0); // year
        values[21] = Bcd1(year, 3); // year
        values[22] = Bcd1(year, 2); // year
        values[23] = Bcd1(year, 1); // year
        values[24] = Bcd1(year, 0); // year

        values[25] = Bcd10(month, 0); // month
        values[26] = Bcd1(month, 3); // month
        values[27] = Bcd1(month, 2); // month
        values[28] = Bcd1(month, 1); // month
        values[29] = Bcd1(month, 0); // month

        values[30] = Bcd10(day, 1); // day
        values[31] = Bcd10(day, 0); // day
        values[32] = Bcd1(day, 3); // day
        values[33] = Bcd1(day, 2); // day
        values[34] = Bcd1(day, 1); // day
        values[35] = Bcd1(day, 0); // day

        values[36] = Bcd1(weekday, 2); // day of week
        values[37] = Bcd1(weekday, 1); // day of week
        values[38] = Bcd1(weekday, 0); // day of week

        values[39] = Bcd10(hour, 1); // hour
        values[40] = Bcd10(hour, 0); // hour
        values[41] = Bcd1(hour, 3); // hour
        values[42] = Bcd1(hour, 2); // hour
        values[43] = Bcd1(hour, 1); // hour
        values[44] = Bcd1(hour, 0); // hour

        values[45] = Bcd10(min, 2); // min
        values[46] = Bcd10(min, 1); // min
        values[47] = Bcd10(min, 0); // min
        values[48] = Bcd1(min, 3); // min
        values[49] = Bcd1(min, 2); // min
        values[50] = Bcd1(min, 1); // min
        values[51] = Bcd1(min, 0); // min

        // 52 - marker
        // 53 - DST change, ignoring

        values[54] = !Parity(values[17..25].AsSpan(), 8); // year odd parity
        values[55] = !Parity(values[25..36].AsSpan(), 11); // month + day odd parity
        values[56] = !Parity(values[36..39].AsSpan(), 3); // day of week odd parity
        values[57] = !Parity(values[39..52].AsSpan(), 13); // hour + min odd parity

        values[58] = isDst;

        // 59 - unused

        return values;
    }

    private static bool Bcd1(int val, int pos) => BitHelper.Bcd1(val, pos);

    private static bool Bcd10(int val, int pos) => BitHelper.Bcd10(val, pos);

    private static bool Parity(ReadOnlySpan<bool> values, int length) => BitHelper.Parity(values, length);
}