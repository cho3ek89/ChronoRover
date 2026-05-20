using ChronoRover.UI.Converters;

using Moq;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.UI.Converters;

[TestFixture]
public class ExceptionToStringConverterTests
{
    private readonly ExceptionToStringConverter _converter = new();

    [Test]
    public void ConvertShouldReturnCorrectString()
    {
        var exceptionMock = new Mock<Exception>();
        exceptionMock.SetupGet(e => e.Message).Returns("dummy message...");
        exceptionMock.SetupGet(e => e.StackTrace).Returns("dummy stack trace...");
        var exception = exceptionMock.Object;

        const string expectedResult = """
                                      Message:
                                      dummy message...


                                      Stack trace:
                                      dummy stack trace...

                                      """;

        var actualResult = _converter.Convert(exception, typeof(string), null, null!);

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }

    [Test]
    public void ConvertShouldReturnNullIfExceptionIsNull()
    {
        Assert.That(
            _converter.Convert(null, typeof(string), null, null!),
            Is.Null);
    }

    [Test]
    public void ConvertBackShouldThrowException()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            _converter.ConvertBack(string.Empty, typeof(Exception), null, null!);
        });
    }
}