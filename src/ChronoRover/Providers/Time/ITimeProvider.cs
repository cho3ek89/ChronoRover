using System;

namespace ChronoRover.Providers.Time;

public interface ITimeProvider
{
    DateTime GetTime();

    DateTime GetUtcTime();
}