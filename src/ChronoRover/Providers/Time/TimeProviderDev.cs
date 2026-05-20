using GuerrillaNtp;

using System;

namespace ChronoRover.Providers.Time;

public class TimeProviderDev(NtpClock clock) : ITimeProvider
{
    public DateTime GetTime() =>
        AddOffset(clock.Now.LocalDateTime);

    public DateTime GetUtcTime() =>
        AddOffset(clock.UtcNow.UtcDateTime);

    private static DateTime AddOffset(DateTime dateTime) =>
        dateTime
            .AddDays(-3)
            .AddHours(-2)
            .AddMinutes(-13)
            .AddSeconds(-26);
}