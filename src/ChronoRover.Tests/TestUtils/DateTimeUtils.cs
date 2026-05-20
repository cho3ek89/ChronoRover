using ChronoRover.Providers.Time;

using Moq;

using System;

namespace ChronoRover.Tests.TestUtils;

public static class DateTimeUtils
{
    public static ITimeProvider GetDefaultTimeProvider(DateTime dateTime, DateTime dateTimeUtc)
    {
        var tzProviderMock = new Mock<ITimeProvider>();
        tzProviderMock
            .Setup(s => s.GetTime())
            .Returns(dateTime);
        tzProviderMock
            .Setup(s => s.GetUtcTime())
            .Returns(dateTimeUtc);

        return tzProviderMock.Object;
    }
}