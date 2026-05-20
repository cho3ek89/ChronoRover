using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using Moq;

using System;

namespace ChronoRover.Tests.TestUtils;

public static class TimeZoneUtils
{
    public static ITimeZoneProvider GetDefaultTimeZoneProvider()
    {
        var tzProviderMock = new Mock<ITimeZoneProvider>();
        tzProviderMock
            .Setup(s => s.GetTimeZone(It.IsAny<SignalType>()))
            .Returns(GetTimeZone());

        return tzProviderMock.Object;
    }

    private static TimeZoneInfo GetTimeZone()
    {
        // last sunday of march (02:00)
        var dstStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(
            timeOfDay: new DateTime(1, 1, 1, 2, 0, 0),
            month: 3,
            week: 5,
            dayOfWeek: DayOfWeek.Sunday);

        // last sunday of october (03:00) 
        var dstEnd = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(
            timeOfDay: new DateTime(1, 1, 1, 3, 0, 0),
            month: 10,
            week: 5,
            dayOfWeek: DayOfWeek.Sunday);

        // apply the rule for a broad period
        var rule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(
            dateStart: DateTime.MinValue,
            dateEnd: DateTime.MaxValue,
            daylightDelta: TimeSpan.FromHours(1),
            daylightTransitionStart: dstStart,
            daylightTransitionEnd: dstEnd);

        var timeZone = TimeZoneInfo.CreateCustomTimeZone(
            id: "Test1/UTC+01",
            baseUtcOffset: TimeSpan.FromHours(1),
            displayName: "(UTC+01:00) Custom Time Zone",
            standardDisplayName: "Custom Time Zone",
            daylightDisplayName: "Custom Time Zone DST",
            adjustmentRules: [rule],
            disableDaylightSavingTime: false);

        return timeZone;
    }
}