using ChronoRover.Models;

using System;

namespace ChronoRover.Providers.TimeZone;

public interface ITimeZoneProvider
{
    TimeZoneInfo GetLocalTimeZone();

    TimeZoneInfo GetTimeZone(SignalType signalType);
}