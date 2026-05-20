using ChronoRover.Models;
using ChronoRover.Providers.Signal;

using System;

namespace ChronoRover.Providers.Wave;

public class WaveDataProviderFactory(ISignalProviderFactory signalProviderFactory) : IWaveDataProviderFactory
{
    public IWaveDataProvider GetWaveDateProvider(SignalType signalType) => signalType switch
    {
        SignalType.Dcf77 => new Dcf77WaveDataProvider(signalProviderFactory),
        SignalType.Wwvb => new WwvbWaveDataProvider(signalProviderFactory),
        SignalType.Jjy => new JjyWaveDataProvider(signalProviderFactory),
        SignalType.Bpc => new BpcWaveDataProvider(signalProviderFactory),
        SignalType.Msf => new MsfWaveDataProvider(signalProviderFactory),
        _ => throw new ArgumentException($"A provider for {signalType} is not implemented.")
    };
}