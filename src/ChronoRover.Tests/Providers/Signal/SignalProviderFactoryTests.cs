using ChronoRover.Models;
using ChronoRover.Providers.Signal;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.Providers.Signal;

[TestFixture]
public class SignalProviderFactoryTests
{
    private readonly SignalProviderFactory _factory = new(TimeZoneUtils.GetDefaultTimeZoneProvider());

    [Test]
    [TestCaseSource(nameof(GetGetSignalProviderReturnsCorrectSignalProviderTestData))]
    public void GetSignalProviderReturnsCorrectSignalProvider(SignalType signalType, Type expectedProviderType)
    {
        var provider = _factory.GetSignalProvider(signalType);

        Assert.That(provider, Is.Not.Null);
        Assert.That(provider, Is.TypeOf(expectedProviderType));
    }

    [Test]
    public void GetSignalProviderThrowsArgumentExceptionForUnsupportedSignalType()
    {
        const SignalType unsupportedType = (SignalType)999;

        var ex = Assert.Throws<ArgumentException>(() => _factory.GetSignalProvider(unsupportedType));
        Assert.That(ex!.Message, Does.Contain($"A provider for 999 is not implemented."));
    }

    #region Test data

    private static object[] GetGetSignalProviderReturnsCorrectSignalProviderTestData()
    {
        object[] t1 = [SignalType.Dcf77, typeof(Dcf77SignalProvider)];
        object[] t2 = [SignalType.Wwvb, typeof(WwvbSignalProvider)];
        object[] t3 = [SignalType.Jjy, typeof(JjySignalProvider)];
        object[] t4 = [SignalType.Bpc, typeof(BpcSignalProvider)];
        object[] t5 = [SignalType.Msf, typeof(MsfSignalProvider)];

        return [t1, t2, t3, t4, t5];
    }

    #endregion Test data
}