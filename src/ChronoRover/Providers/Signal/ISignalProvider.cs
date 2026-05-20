using System;

namespace ChronoRover.Providers.Signal;

public interface ISignalProvider
{
    bool[] GetMinuteSignal(DateTime dateTime);
}