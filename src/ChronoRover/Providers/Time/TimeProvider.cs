using GuerrillaNtp;

using System;

namespace ChronoRover.Providers.Time;

public class TimeProvider(NtpClock clock) : ITimeProvider
{
    public DateTime GetTime() => clock.Now.LocalDateTime;

    public DateTime GetUtcTime() => clock.UtcNow.UtcDateTime;
}