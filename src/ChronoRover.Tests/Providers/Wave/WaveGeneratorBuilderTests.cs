using ChronoRover.Models;
using ChronoRover.Providers.Time;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Providers.Wave;
using ChronoRover.Services.Settings;
using ChronoRover.Services.Wave;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using SoundFlow.Abstracts;
using SoundFlow.Structs;

using System;

namespace ChronoRover.Tests.Providers.Wave;

[TestFixture]
public class WaveGeneratorBuilderTests
{
    private Mock<AudioEngine> _audioEngine;
    private Mock<ILogger<WaveGenerator>> _loggerMock;
    private Mock<ITimeProvider> _timeProviderMock;
    private Mock<IMinuteEnumerableFactory> _minuteEnumerableFactoryMock;
    private Mock<IWaveDataProviderFactory> _waveDataProviderFactoryMock;
    private Mock<ISettingsManager> _settingsManagerMock;

    private WaveGeneratorBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _audioEngine = new Mock<AudioEngine>();
        _loggerMock = new Mock<ILogger<WaveGenerator>>();
        _timeProviderMock = new Mock<ITimeProvider>();
        _minuteEnumerableFactoryMock = new Mock<IMinuteEnumerableFactory>();
        _waveDataProviderFactoryMock = new Mock<IWaveDataProviderFactory>();
        _settingsManagerMock = new Mock<ISettingsManager>();

        _builder = new WaveGeneratorBuilder(
            _audioEngine.Object,
            _loggerMock.Object,
            _timeProviderMock.Object,
            _minuteEnumerableFactoryMock.Object,
            _waveDataProviderFactoryMock.Object);
    }

    [Test]
    public void InitFromSettingsSetsFormatAndSignalTypeFromSettingsManager()
    {
        var format = AudioFormat.Broadcast;
        const SignalType signalType = SignalType.Wwvb;

        _settingsManagerMock.SetupGet(s => s.AudioFormat)
            .Returns(format)
            .Verifiable();

        _settingsManagerMock
            .SetupGet(s => s.SignalType)
            .Returns(signalType)
            .Verifiable();

        _builder.InitFromSettings(_settingsManagerMock.Object);

        _settingsManagerMock.Verify();
    }

    [Test]
    public void BuildConstructsWaveGeneratorWithCorrectAudioFormat()
    {
        var format = AudioFormat.Broadcast;
        _builder.WithAudioFormat(format);

        var generator = _builder.Build();
        Assert.That(generator.Format, Is.EqualTo(format));
    }

    [Test]
    public void BuildConstructsWaveGeneratorWithCorrectSignalType()
    {
        const SignalType signalType = SignalType.Bpc;
        _builder.WithSignalType(signalType);

        _waveDataProviderFactoryMock
            .Setup(f => f.GetWaveDateProvider(signalType))
            .Returns(new Mock<IWaveDataProvider>().Object)
            .Verifiable();

        _minuteEnumerableFactoryMock
            .Setup(f => f.GetMinuteEnumerable(signalType))
            .Returns(new Mock<IMinuteEnumerable>().Object)
            .Verifiable();

        _builder.Build();

        // A little bit indirect check but still...
        _waveDataProviderFactoryMock.Verify();
        _minuteEnumerableFactoryMock.Verify();
    }

    [Test]
    public void BuildThrowsArgumentNullExceptionWhenEngineIsNull()
    {
        var builderWithNullEngine = new WaveGeneratorBuilder(
            null,
            _loggerMock.Object,
            _timeProviderMock.Object,
            _minuteEnumerableFactoryMock.Object,
            _waveDataProviderFactoryMock.Object);

        var ex = Assert.Throws<ArgumentNullException>(() => builderWithNullEngine.Build());
        Assert.That(ex!.Message, Does.Contain("engine"));
    }

    [Test]
    public void BuildThrowsArgumentNullExceptionWhenTimeProviderIsNull()
    {
        var builderWithNullTimeProvider = new WaveGeneratorBuilder(
            _audioEngine.Object,
            _loggerMock.Object,
            null,
            _minuteEnumerableFactoryMock.Object,
            _waveDataProviderFactoryMock.Object);

        var ex = Assert.Throws<ArgumentNullException>(() => builderWithNullTimeProvider.Build());
        Assert.That(ex!.Message, Does.Contain("timeProvider"));
    }

    [Test]
    public void BuildThrowsArgumentNullExceptionWhenMinuteEnumerableFactoryIsNull()
    {
        var builderWithNullTimeProvider = new WaveGeneratorBuilder(
            _audioEngine.Object,
            _loggerMock.Object,
            _timeProviderMock.Object,
            null,
            _waveDataProviderFactoryMock.Object);

        var ex = Assert.Throws<ArgumentNullException>(() => builderWithNullTimeProvider.Build());
        Assert.That(ex!.Message, Does.Contain("minuteEnumerableFactory"));
    }

    [Test]
    public void BuildThrowsArgumentNullExceptionWhenWaveDataProviderFactoryIsNull()
    {
        var builderWithNullTimeProvider = new WaveGeneratorBuilder(
            _audioEngine.Object,
            _loggerMock.Object,
            _timeProviderMock.Object,
            _minuteEnumerableFactoryMock.Object,
            null);

        var ex = Assert.Throws<ArgumentNullException>(() => builderWithNullTimeProvider.Build());
        Assert.That(ex!.Message, Does.Contain("waveDataProviderFactory"));
    }
}