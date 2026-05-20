using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Time.TimeWindow;

public class Dcf77MinuteEnumerable(
    ITimeProvider timeProvider,
    ITimeZoneProvider timeZoneProvider) : MinuteEnumerable
{
    protected override void Initialize()
    {
        Minute = TimeZoneInfo.ConvertTimeFromUtc(
            timeProvider.GetUtcTime(),
            timeZoneProvider.GetTimeZone(SignalType.Dcf77));

        Minute = Minute.AddMinutes(1);
    }
}