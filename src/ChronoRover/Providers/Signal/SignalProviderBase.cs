using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Signal;

public abstract class SignalProviderBase(
    ITimeZoneProvider timeZoneProvider,
    SignalType signalType) : ISignalProvider
{
    protected readonly TimeZoneInfo TimeZone = timeZoneProvider.GetTimeZone(signalType);

    public abstract bool[] GetMinuteSignal(DateTime dateTime);
}