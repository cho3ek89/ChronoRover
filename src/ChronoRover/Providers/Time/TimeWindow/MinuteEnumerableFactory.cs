using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Time.TimeWindow;

public class MinuteEnumerableFactory(
    ITimeProvider timeProvider,
    ITimeZoneProvider timeZoneProvider) : IMinuteEnumerableFactory
{
    public IMinuteEnumerable GetMinuteEnumerable(SignalType signalType)
    {
        return signalType switch
        {
            SignalType.Dcf77 => new Dcf77MinuteEnumerable(timeProvider, timeZoneProvider),
            SignalType.Wwvb => new WwvbMinuteEnumerable(timeProvider),
            SignalType.Jjy => new JjyMinuteEnumerable(timeProvider, timeZoneProvider),
            SignalType.Bpc => new BpcMinuteEnumerable(timeProvider, timeZoneProvider),
            SignalType.Msf => new MsfMinuteEnumerable(timeProvider, timeZoneProvider),
            _ => throw new ArgumentException(@$"{signalType} is not supported!", nameof(signalType))
        };
    }
}