using ChronoRover.Models;
using ChronoRover.Providers.Signal;

using System;

namespace ChronoRover.Providers.Wave;

public abstract class WaveDataProviderBase(
    ISignalProviderFactory signalProviderFactory,
    SignalType signalType) : IWaveDataProvider
{
    protected readonly ISignalProvider SignalProvider = signalProviderFactory.GetSignalProvider(signalType);

    public abstract short[] GetWaveData(int sampleRate, DateTime dateTime, bool stripPassedMs = false);
}