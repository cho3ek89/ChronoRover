using ChronoRover.Models;
using ChronoRover.Providers.Settings;
using ChronoRover.Providers.TimeZone;

using Moq;

using NUnit.Framework;

using SoundFlow.Enums;
using SoundFlow.Structs;

using System;

namespace ChronoRover.Tests.Providers.Settings;

[TestFixture]
public class DefaultSettingsProviderTests
{
    [Test]
    public void GetAudioFormatReturnsCorrectResult()
    {
        var settingsProvider = new DefaultSettingsProvider(Mock.Of<ITimeZoneProvider>());

        var actualAudioFormat = settingsProvider.GetAudioFormat();

        var expectedAudioFormat = new AudioFormat
        {
            Format = SampleFormat.S16,
            Channels = 1,
            Layout = ChannelLayout.Mono,
            SampleRate = 44100,
        };

        Assert.That(actualAudioFormat, Is.EqualTo(expectedAudioFormat));
    }

    [Test]
    [TestCaseSource(nameof(GetSignalTypeReturnsCorrectResultTestData))]
    public void GetSignalTypeReturnsCorrectResult(double offset, SignalType expectedSignalType)
    {
        var timeZoneProviderMock = new Mock<ITimeZoneProvider>();
        timeZoneProviderMock.Setup(s => s.GetLocalTimeZone())
            .Returns(TimeZoneInfo.CreateCustomTimeZone("id", TimeSpan.FromHours(offset), "", ""));

        var settingsProvider = new DefaultSettingsProvider(timeZoneProviderMock.Object);

        var actualSignalType = settingsProvider.GetSignalType();

        Assert.That(actualSignalType, Is.EqualTo(expectedSignalType));
    }

    #region Test data

    private static object[] GetSignalTypeReturnsCorrectResultTestData()
    {
        object[] tz14M = [-14d, SignalType.Wwvb];
        object[] tz13M = [-13d, SignalType.Wwvb];
        object[] tz12M = [-12d, SignalType.Wwvb];
        object[] tz11M = [-11d, SignalType.Wwvb];
        object[] tz10M = [-10d, SignalType.Wwvb];
        object[] tz09M = [-09d, SignalType.Wwvb];
        object[] tz08M = [-08d, SignalType.Wwvb];
        object[] tz07M = [-07d, SignalType.Wwvb];
        object[] tz06M = [-06d, SignalType.Wwvb];
        object[] tz05M = [-05d, SignalType.Wwvb];
        object[] tz04M = [-04d, SignalType.Wwvb];
        object[] tz03M = [-03d, SignalType.Wwvb];
        object[] tz02M = [-02d, SignalType.Wwvb];
        object[] tz01M = [-01d, SignalType.Msf];
        object[] tz00P = [+00d, SignalType.Msf];
        object[] tz01P = [+01d, SignalType.Dcf77];
        object[] tz02P = [+02d, SignalType.Dcf77];
        object[] tz03P = [+03d, SignalType.Dcf77];
        object[] tz04P = [+04d, SignalType.Bpc];
        object[] tz05P = [+05d, SignalType.Bpc];
        object[] tz06P = [+06d, SignalType.Bpc];
        object[] tz07P = [+07d, SignalType.Bpc];
        object[] tz08P = [+08d, SignalType.Bpc];
        object[] tz09P = [+09d, SignalType.Jjy];
        object[] tz10P = [+10d, SignalType.Jjy];
        object[] tz11P = [+11d, SignalType.Jjy];
        object[] tz12P = [+12d, SignalType.Jjy];
        object[] tz13P = [+13d, SignalType.Jjy];
        object[] tz14P = [+14d, SignalType.Jjy];

        return
        [
            tz14M, tz13M, tz12M, tz11M, tz10M, tz09M, tz08M, tz07M, tz06M, tz05M, tz04M, tz03M, tz02M, tz01M,
            tz00P,
            tz01P, tz02P, tz03P, tz04P, tz05P, tz06P, tz07P, tz08P, tz09P, tz10P, tz11P, tz12P, tz13P, tz14P,
        ];
    }

    #endregion Test data
}