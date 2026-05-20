using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using System;

namespace ChronoRover.Providers.Signal;

public class SignalProviderFactory(ITimeZoneProvider timeZoneProvider) : ISignalProviderFactory
{
    public ISignalProvider GetSignalProvider(SignalType signalType) => signalType switch
    {
        SignalType.Dcf77 => new Dcf77SignalProvider(timeZoneProvider),
        SignalType.Wwvb => new WwvbSignalProvider(timeZoneProvider),
        SignalType.Jjy => new JjySignalProvider(timeZoneProvider),
        SignalType.Bpc => new BpcSignalProvider(timeZoneProvider),
        SignalType.Msf => new MsfSignalProvider(timeZoneProvider),
        _ => throw new ArgumentException($"A provider for {signalType} is not implemented.")
    };
}