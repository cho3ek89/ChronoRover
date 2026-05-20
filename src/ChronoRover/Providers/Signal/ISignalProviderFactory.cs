using ChronoRover.Models;

namespace ChronoRover.Providers.Signal;

public interface ISignalProviderFactory
{
    ISignalProvider GetSignalProvider(SignalType signalType);
}