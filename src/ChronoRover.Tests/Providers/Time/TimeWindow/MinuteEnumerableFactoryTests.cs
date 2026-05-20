using ChronoRover.Models;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.Providers.Time.TimeWindow;

[TestFixture]
public class MinuteEnumerableFactoryTests
{
    private readonly MinuteEnumerableFactory _factory = new(
        DateTimeUtils.GetDefaultTimeProvider(DateTime.MinValue, DateTime.MinValue),
        TimeZoneUtils.GetDefaultTimeZoneProvider());

    [Test]
    [TestCaseSource(nameof(GetGetMinuteEnumerableReturnsCorrectMinuteEnumerableTestData))]
    public void GetMinuteEnumerableReturnsCorrectMinuteEnumerable(
        SignalType signalType, Type expectedMinuteEnumerableType)
    {
        var minuteEnumerable = _factory.GetMinuteEnumerable(signalType);

        Assert.That(minuteEnumerable, Is.Not.Null);
        Assert.That(minuteEnumerable, Is.TypeOf(expectedMinuteEnumerableType));
    }

    [Test]
    public void GetMinuteEnumerableThrowsArgumentExceptionForUnsupportedSignalType()
    {
        const SignalType unsupportedType = (SignalType)999;

        var ex = Assert.Throws<ArgumentException>(() => _factory.GetMinuteEnumerable(unsupportedType));
        Assert.That(ex!.Message, Does.Contain("999 is not supported!"));
    }

    #region Test data

    private static object[] GetGetMinuteEnumerableReturnsCorrectMinuteEnumerableTestData()
    {
        object[] t1 = [SignalType.Dcf77, typeof(Dcf77MinuteEnumerable)];
        object[] t2 = [SignalType.Wwvb, typeof(WwvbMinuteEnumerable)];
        object[] t3 = [SignalType.Jjy, typeof(JjyMinuteEnumerable)];
        object[] t4 = [SignalType.Bpc, typeof(BpcMinuteEnumerable)];
        object[] t5 = [SignalType.Msf, typeof(MsfMinuteEnumerable)];

        return [t1, t2, t3, t4, t5];
    }

    #endregion Test data
}