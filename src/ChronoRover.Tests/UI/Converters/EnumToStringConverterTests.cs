using ChronoRover.UI.Converters;

using NUnit.Framework;

using System;
using System.IO;

namespace ChronoRover.Tests.UI.Converters;

[TestFixture]
public class EnumToStringConverterTests
{
    private readonly EnumToStringConverter _converter = new();

    [TestCase(DayOfWeek.Monday, "MONDAY")]
    [TestCase(ConsoleColor.Yellow, "YELLOW")]
    [TestCase(FileMode.Truncate, "TRUNCATE")]
    public void ConvertShouldReturnUppercaseEnumName(object value, string expectedValue)
    {
        var result = _converter.Convert(value, typeof(string), null, null!);

        Assert.That(result, Is.EqualTo(expectedValue));
    }

    [Test]
    public void ConvertShouldThrowExceptionWhenValueIsNotEnum()
    {
        Assert.Throws<ArgumentException>(() => { _converter.Convert("non num value", typeof(string), null, null!); });
    }

    [Test]
    public void ConvertShouldThrowExceptionWhenValueIsNull()
    {
        Assert.Throws<NullReferenceException>(() => { _converter.Convert(null, typeof(string), null, null!); });
    }

    [Test]
    public void ConvertBackShouldThrowException()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            _converter.ConvertBack("MONDAY", typeof(DayOfWeek), null, null!);
        });
    }
}