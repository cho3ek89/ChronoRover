using ChronoRover.Providers.Time.TimeWindow;

using NUnit.Framework;

using System;
using System.Linq;

namespace ChronoRover.Tests.Providers.Time.TimeWindow;

[TestFixture]
public class WwvbMinuteEnumerableTests
{
    [Test]
    [TestCaseSource(nameof(GetGetEnumeratorReturnsCorrectValuesTestData))]
    public void GetEnumeratorReturnsCorrectValues(DateTime utcDateTime, DateTime[] expectedDateTimes)
    {
        var timeProvider = TestUtils.DateTimeUtils.GetDefaultTimeProvider(DateTime.MinValue, utcDateTime);
        var minuteEnumerable = new WwvbMinuteEnumerable(timeProvider);

        var actualDateTimes = minuteEnumerable.Take(5).ToList();

        Assert.That(actualDateTimes, Is.EqualTo(expectedDateTimes));
    }

    #region Test data

    private static object[] GetGetEnumeratorReturnsCorrectValuesTestData()
    {
        object[] tc1 =
        [
            new DateTime(2020, 3, 2, 0, 20, 20, 222),
            new DateTime[]
            {
                new(2020, 3, 2, 0, 20, 20, 222),
                new(2020, 3, 2, 0, 21, 20, 222),
                new(2020, 3, 2, 0, 22, 20, 222),
                new(2020, 3, 2, 0, 23, 20, 222),
                new(2020, 3, 2, 0, 24, 20, 222),
            }
        ];

        return [tc1];
    }

    #endregion Test data
}