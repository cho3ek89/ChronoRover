using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Time.TimeWindow;

public class JjyMinuteEnumerable(
    ITimeProvider timeProvider,
    ITimeZoneProvider timeZoneProvider) : MinuteEnumerable
{
    protected override void Initialize()
    {
        Minute = TimeZoneInfo.ConvertTimeFromUtc(
            timeProvider.GetUtcTime(),
            timeZoneProvider.GetTimeZone(SignalType.Jjy));
    }
}