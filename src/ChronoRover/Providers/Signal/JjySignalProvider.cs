using ChronoRover.Models;
using ChronoRover.Helpers;
using ChronoRover.Providers.TimeZone;

using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Signal;

public class JjySignalProvider(
    ITimeZoneProvider timeZoneProvider) : SignalProviderBase(timeZoneProvider, SignalType.Jjy)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool[] GetMinuteSignal(DateTime dateTime)
    {
        var values = new bool[60];

        var min = dateTime.Minute;
        var hour = dateTime.Hour;
        var doy = dateTime.DayOfYear;
        var year = dateTime.Year % 100;
        var weekday = (int)dateTime.DayOfWeek;

        //     Setting values.

        //     00 - marker
        values[01] = Bcd10(min, 2); // min
        values[02] = Bcd10(min, 1); // min
        values[03] = Bcd10(min, 0); // min
        //     04 - unused
        values[05] = Bcd1(min, 3); // min
        values[06] = Bcd1(min, 2); // min
        values[07] = Bcd1(min, 1); // min
        values[08] = Bcd1(min, 0); // min
        //     09 - marker
        //     10 - unused
        //     11 - unused
        values[12] = Bcd10(hour, 1); // hour
        values[13] = Bcd10(hour, 0); // hour
        //     14 - unused
        values[15] = Bcd1(hour, 3); // hour
        values[16] = Bcd1(hour, 2); // hour
        values[17] = Bcd1(hour, 1); // hour
        values[18] = Bcd1(hour, 0); // hour
        //     19 - marker
        //     20 - unused
        //     21 - unused
        values[22] = Bcd100(doy, 1); // doy
        values[23] = Bcd100(doy, 0); // doy
        //     24 - unused
        values[25] = Bcd10(doy, 3); // doy
        values[26] = Bcd10(doy, 2); // doy
        values[27] = Bcd10(doy, 1); // doy
        values[28] = Bcd10(doy, 0); // doy
        //     29 - marker
        values[30] = Bcd1(doy, 3); // doy
        values[31] = Bcd1(doy, 2); // doy
        values[32] = Bcd1(doy, 1); // doy
        values[33] = Bcd1(doy, 0); // doy
        //     34 - unused
        //     35 - unused
        values[36] = Parity(values[12..14].Concat(values[15..19]).ToArray(), 6); // hour parity
        values[37] = Parity(values[01..04].Concat(values[05..09]).ToArray(), 7); // min parity
        //     38 - unused
        //     39 - marker
        //     40 - unused
        values[41] = Bcd10(year, 3); // year
        values[42] = Bcd10(year, 2); // year
        values[43] = Bcd10(year, 1); // year
        values[44] = Bcd10(year, 0); // year
        values[45] = Bcd1(year, 3); // year
        values[46] = Bcd1(year, 2); // year
        values[47] = Bcd1(year, 1); // year
        values[48] = Bcd1(year, 0); // year
        //     49 - marker
        values[50] = Bcd1(weekday, 2); // day of week
        values[51] = Bcd1(weekday, 1); // day of week
        values[52] = Bcd1(weekday, 0); // day of week
        //     53 - ignoring leap second
        //     54 - ignoring leap second type (added/deleted)
        //     55 - unused
        //     56 - unused
        //     57 - unused
        //     58 - unused
        //     59 - marker

        return values;
    }

    private static bool Bcd1(int val, int pos) => BitHelper.Bcd1(val, pos);

    private static bool Bcd10(int val, int pos) => BitHelper.Bcd10(val, pos);

    private static bool Bcd100(int val, int pos) => BitHelper.Bcd100(val, pos);

    private static bool Parity(ReadOnlySpan<bool> values, int length) => BitHelper.Parity(values, length);
}