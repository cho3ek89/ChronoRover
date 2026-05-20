using ChronoRover.Models;
using ChronoRover.Helpers;
using ChronoRover.Providers.TimeZone;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Signal;

public class WwvbSignalProvider(
    ITimeZoneProvider timeZoneProvider) : SignalProviderBase(timeZoneProvider, SignalType.Wwvb)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool[] GetMinuteSignal(DateTime dateTime)
    {
        var values = new bool[60];

        var min = dateTime.Minute;
        var hour = dateTime.Hour;
        var doy = dateTime.DayOfYear;
        var year = dateTime.Year % 100;
        var isLeapYear = DateTime.IsLeapYear(dateTime.Year);
        var isDst = TimeZone.IsDaylightSavingTime(dateTime);

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
        //     36           // DUT1 sign (-)
        values[37] = true;  // DUT1 sign (-)
        //     38           // DUT1 sign (-)
        //     39 - marker
        //     40           // DUT1 - 0
        //     41           // DUT1 - 0
        //     42           // DUT1 - 0
        //     43           // DUT1 - 0
        //     44 - unused
        values[45] = Bcd10(year, 3); // year
        values[46] = Bcd10(year, 2); // year
        values[47] = Bcd10(year, 1); // year
        values[48] = Bcd10(year, 0); // year
        //     49 - marker
        values[50] = Bcd1(year, 3); // year
        values[51] = Bcd1(year, 2); // year
        values[52] = Bcd1(year, 1); // year
        values[53] = Bcd1(year, 0); // year
        //     54 - unused
        values[55] = isLeapYear;
        //     56 - ignoring leap second announcement
        values[57] = isDst; // is DST, ignoring DSTs begin and end dates announcement
        values[58] = isDst; // is DST, ignoring DSTs begin and end dates announcement
        //     59 - marker

        return values;
    }

    private static bool Bcd1(int val, int pos) => BitHelper.Bcd1(val, pos);

    private static bool Bcd10(int val, int pos) => BitHelper.Bcd10(val, pos);

    private static bool Bcd100(int val, int pos) => BitHelper.Bcd100(val, pos);
}