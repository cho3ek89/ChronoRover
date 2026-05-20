using ChronoRover.UI.Converters;

using NUnit.Framework;

using System;
using System.Globalization;

namespace ChronoRover.Tests.UI.Converters;

[TestFixture]
public class DateTimeToStringConverterTests
{
    private readonly DateTimeToStringConverter _converter = new();

    [TestCase(null, "12/25/2025 14:30:45")]
    [TestCase("yyy-MM-dd (HH:mm:ss)", "2025-12-25 (14:30:45)")]
    [TestCase("o", "2025-12-25T14:30:45.0000000")]
    public void ConvertShouldReturnDateTimeInString(string parameter, string expectedValue)
    {
        var date = new DateTime(2025, 12, 25, 14, 30, 45);

        Assert.That(
            _converter.Convert(date, typeof(string), parameter, CultureInfo.InvariantCulture),
            Is.EqualTo(expectedValue));
    }

    [Test]
    public void ConvertShouldThrowExceptionWhenValueIsNotDateTime()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            _converter.Convert(2024, typeof(string), null, CultureInfo.InvariantCulture);
        });
    }

    [Test]
    public void ConvertShouldThrowExceptionWhenValueIsNull()
    {
        Assert.Throws<NullReferenceException>(() =>
        {
            _converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture);
        });
    }

    [Test]
    public void ConvertBackShouldThrowException()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            _converter.ConvertBack("2025-04-07", typeof(DateTime), null, CultureInfo.InvariantCulture);
        });
    }
}