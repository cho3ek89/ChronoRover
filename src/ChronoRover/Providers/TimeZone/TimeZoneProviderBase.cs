using ChronoRover.Models;

using System;

namespace ChronoRover.Providers.TimeZone;

public abstract class TimeZoneProviderBase : ITimeZoneProvider
{
    public TimeZoneInfo GetLocalTimeZone() => TimeZoneInfo.Local;

    public TimeZoneInfo GetTimeZone(SignalType signalType)
    {
        return signalType switch
        {
            SignalType.Dcf77 => TimeZoneInfo.FindSystemTimeZoneById(GetDcf77TimeZoneId()),
            SignalType.Wwvb => TimeZoneInfo.FindSystemTimeZoneById(GetWwvbTimeZoneId()),
            SignalType.Jjy => TimeZoneInfo.FindSystemTimeZoneById(GetJjyTimeZoneId()),
            SignalType.Bpc => TimeZoneInfo.FindSystemTimeZoneById(GetBpcTimeZoneId()),
            SignalType.Msf => TimeZoneInfo.FindSystemTimeZoneById(GetMsfTimeZoneId()),
            _ => throw new TimeZoneNotFoundException($"A time zone for {signalType} signal type was not found.")
        };
    }

    protected abstract string GetDcf77TimeZoneId();

    protected abstract string GetWwvbTimeZoneId();

    protected abstract string GetJjyTimeZoneId();

    protected abstract string GetBpcTimeZoneId();

    protected abstract string GetMsfTimeZoneId();
}