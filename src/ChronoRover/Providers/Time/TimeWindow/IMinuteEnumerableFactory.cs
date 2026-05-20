using ChronoRover.Models;

namespace ChronoRover.Providers.Time.TimeWindow;

public interface IMinuteEnumerableFactory
{
    IMinuteEnumerable GetMinuteEnumerable(SignalType signalType);
}