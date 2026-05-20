using ChronoRover.Models;

namespace ChronoRover.Providers.Wave;

public interface IWaveDataProviderFactory
{
    IWaveDataProvider GetWaveDateProvider(SignalType signalType);
}