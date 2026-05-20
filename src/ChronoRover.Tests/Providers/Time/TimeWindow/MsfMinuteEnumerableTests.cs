using ChronoRover.Providers.Time.TimeWindow;

using NUnit.Framework;

using System;
using System.Linq;

namespace ChronoRover.Tests.Providers.Time.TimeWindow;

[TestFixture]
public class MsfMinuteEnumerableTests
{
    [Test]
    [TestCaseSource(nameof(GetGetEnumeratorReturnsCorrectValuesTestData))]
    public void GetEnumeratorReturnsCorrectValues(DateTime utcDateTime, DateTime[] expectedDateTimes)
    {
        var timeZoneProvider = TestUtils.TimeZoneUtils.GetDefaultTimeZoneProvider();
        var timeProvider = TestUtils.DateTimeUtils.GetDefaultTimeProvider(DateTime.MinValue, utcDateTime);
        var minuteEnumerable = new MsfMinuteEnumerable(timeProvider, timeZoneProvider);

        var actualDateTimes = minuteEnumerable.Take(5).ToList();

        Assert.That(actualDateTimes, Is.EqualTo(expectedDateTimes));
    }

    #region Test data

    private static object[] GetGetEnumeratorReturnsCorrectValuesTestData()
    {
        // no DST
        object[] tc1 =
        [
            new DateTime(2020, 3, 2, 0, 20, 20, 222),
            new DateTime[]
            {
                new(2020, 3, 2, 1, 21, 20, 222),
                new(2020, 3, 2, 1, 22, 20, 222),
                new(2020, 3, 2, 1, 23, 20, 222),
                new(2020, 3, 2, 1, 24, 20, 222),
                new(2020, 3, 2, 1, 25, 20, 222),
            }
        ];

        // is DST
        object[] tc2 =
        [
            new DateTime(2020, 4, 2, 0, 20, 20, 222),
            new DateTime[]
            {
                new(2020, 4, 2, 2, 21, 20, 222),
                new(2020, 4, 2, 2, 22, 20, 222),
                new(2020, 4, 2, 2, 23, 20, 222),
                new(2020, 4, 2, 2, 24, 20, 222),
                new(2020, 4, 2, 2, 25, 20, 222),
            }
        ];

        return [tc1, tc2];
    }

    #endregion Test data
}