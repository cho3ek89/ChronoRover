using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Time.TimeWindow;

public class BpcMinuteEnumerable(
    ITimeProvider timeProvider,
    ITimeZoneProvider timeZoneProvider) : MinuteEnumerable
{
    protected override void Initialize()
    {
        Minute = TimeZoneInfo.ConvertTimeFromUtc(
            timeProvider.GetUtcTime(),
            timeZoneProvider.GetTimeZone(SignalType.Bpc));

        //HACK: It has to be doe because the marker was moved to the back of a minute in 'BpcWaveDataProvider'.
        Minute = Minute.AddSeconds(-1);
    }
}