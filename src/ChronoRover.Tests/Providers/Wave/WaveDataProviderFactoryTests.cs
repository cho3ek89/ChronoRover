using ChronoRover.Models;
using ChronoRover.Providers.Signal;
using ChronoRover.Providers.Wave;

using Moq;

using NUnit.Framework;

using System;

namespace ChronoRover.Tests.Providers.Wave;

[TestFixture]
public class WaveDataProviderFactoryTests
{
    private readonly WaveDataProviderFactory _factory = new(Mock.Of<ISignalProviderFactory>());

    [Test]
    [TestCaseSource(nameof(GetGetWaveDateProviderReturnsCorrectWaveDateProviderTestData))]
    public void GetWaveDateProviderReturnsCorrectWaveDateProvider(SignalType signalType, Type expectedProviderType)
    {
        var provider = _factory.GetWaveDateProvider(signalType);

        Assert.That(provider, Is.Not.Null);
        Assert.That(provider, Is.TypeOf(expectedProviderType));
    }

    [Test]
    public void GetWaveDateProviderThrowsArgumentExceptionForUnsupportedSignalType()
    {
        const SignalType unsupportedType = (SignalType)999;

        var ex = Assert.Throws<ArgumentException>(() => _factory.GetWaveDateProvider(unsupportedType));
        Assert.That(ex!.Message, Does.Contain($"A provider for 999 is not implemented."));
    }

    #region Test data

    private static object[] GetGetWaveDateProviderReturnsCorrectWaveDateProviderTestData()
    {
        object[] t1 = [SignalType.Dcf77, typeof(Dcf77WaveDataProvider)];
        object[] t2 = [SignalType.Wwvb, typeof(WwvbWaveDataProvider)];
        object[] t3 = [SignalType.Jjy, typeof(JjyWaveDataProvider)];
        object[] t4 = [SignalType.Bpc, typeof(BpcWaveDataProvider)];
        object[] t5 = [SignalType.Msf, typeof(MsfWaveDataProvider)];

        return [t1, t2, t3, t4, t5];
    }

    #endregion Test data
}