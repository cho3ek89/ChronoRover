using ChronoRover.Providers.Time;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Providers.Wave;
using ChronoRover.Services.Wave;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using SoundFlow.Abstracts;
using SoundFlow.Structs;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ChronoRover.Tests.Services.Wave;

[TestFixture]
public class WaveGeneratorTests
{
    private const double Tolerance = 0.000000001;

    private readonly DateTime _minute6 = new(2000, 1, 1, 22, 06, 02, 100);
    private readonly DateTime _minute7 = new(2000, 1, 1, 22, 07, 00, 000);
    private readonly DateTime _minute8 = new(2000, 1, 1, 22, 08, 00, 000);

    private readonly short[] _samples6 = [32_100, 32_200, 32_300, 32_400];
    private readonly short[] _samples7 = [31_100, 31_200, 31_300, 31_400];
    private readonly short[] _samples8 = [30_100, 30_200, 30_300, 30_400];

    private readonly AudioFormat _audioFormat;

    public WaveGeneratorTests()
    {
        _audioFormat = AudioFormat.Broadcast;
        _audioFormat.SampleRate = 4;
    }

    /// <summary>
    /// Test checks if propper amount of samples if cut from the first minute's wave data.
    /// </summary>
    /// <param name="firstMinGenTime">The amount of time (in milliseconds) it takes to generate the first minute.</param>
    /// <param name="samplesToSkip">The amount of samples to skip from the first minute.</param>
    [Test]
    [TestCase(250, 1)]
    [TestCase(499, 1)]
    [TestCase(500, 2)]
    [TestCase(749, 2)]
    [TestCase(750, 3)]
    [SuppressMessage("ReSharper", "InlineTemporaryVariable")]
    public void GenerateAudioCutsSamplesFromFirstMinuteCorrectly(int firstMinGenTime, int samplesToSkip)
    {
        var waveDataProviderMock = GetWaveDataProviderMock();
        var minuteEnumerableMock = GetMinuteEnumerableMock();

        var waveGenerator = new WaveGeneratorTestable(
            _audioFormat,
            GetTimeProviderMock(_minute6, _minute6.AddMilliseconds(firstMinGenTime)).Object,
            minuteEnumerableMock.Object,
            waveDataProviderMock.Object);

        var buffer6 = new float[4];
        var buffer7 = new float[4];
        var buffer8 = new float[4];

        waveGenerator.GenerateAudioExecute(buffer6);
        waveGenerator.GenerateAudioExecute(buffer7);
        waveGenerator.GenerateAudioExecute(buffer8);

        var i = samplesToSkip;
        Assert.That(
            buffer6,
            Is.EqualTo(GetDenormalizedSamples([.._samples6[i..], .._samples7[..i]])).Within(Tolerance));

        Assert.That(
            buffer7,
            Is.EqualTo(GetDenormalizedSamples([.._samples7[i..], .._samples8[..i]])).Within(Tolerance));

        minuteEnumerableMock.VerifyAll();
        waveDataProviderMock.VerifyAll();
    }

    /// <summary>
    /// Test checks if ALL wave samples of every consecutive
    /// minute are saved to every consecutive buffer, sample by sample.
    /// <br/>Wave samples array sizes are EQUAL TO buffer sizes.
    /// </summary>
    [Test]
    public void GenerateAudioFillsBuffersWithSamplesCorrectly1()
    {
        var waveDataProviderMock = GetWaveDataProviderMock();
        var minuteEnumerableMock = GetMinuteEnumerableMock();

        var waveGenerator = new WaveGeneratorTestable(
            _audioFormat,
            GetTimeProviderMock(_minute6, _minute6).Object,
            minuteEnumerableMock.Object,
            waveDataProviderMock.Object);

        var buffer6 = new float[4];
        var buffer7 = new float[4];
        var buffer8 = new float[4];

        waveGenerator.GenerateAudioExecute(buffer6);
        waveGenerator.GenerateAudioExecute(buffer7);
        waveGenerator.GenerateAudioExecute(buffer8);

        Assert.That(
            buffer6,
            Is.EqualTo(GetDenormalizedSamples(_samples6)).Within(Tolerance));

        Assert.That(
            buffer7,
            Is.EqualTo(GetDenormalizedSamples(_samples7)).Within(Tolerance));

        Assert.That(
            buffer8,
            Is.EqualTo(GetDenormalizedSamples(_samples8)).Within(Tolerance));

        minuteEnumerableMock.VerifyAll();
        waveDataProviderMock.VerifyAll();
    }

    /// <summary>
    /// Test checks if ALL wave samples of every consecutive
    /// minute are saved to every consecutive buffer, sample by sample.
    /// <br/>Wave samples array sizes are LESSER THAN buffer sizes.
    /// </summary>
    [Test]
    public void GenerateAudioFillsBuffersWithSamplesCorrectly2()
    {
        var waveDataProviderMock = GetWaveDataProviderMock();
        var minuteEnumerableMock = GetMinuteEnumerableMock();

        var waveGenerator = new WaveGeneratorTestable(
            _audioFormat,
            GetTimeProviderMock(_minute6, _minute6).Object,
            minuteEnumerableMock.Object,
            waveDataProviderMock.Object);

        var buffer6 = new float[3];
        var buffer7 = new float[3];
        var buffer8 = new float[3];

        waveGenerator.GenerateAudioExecute(buffer6);
        waveGenerator.GenerateAudioExecute(buffer7);
        waveGenerator.GenerateAudioExecute(buffer8);

        Assert.That(
            buffer6,
            Is.EqualTo(GetDenormalizedSamples(_samples6[..3])).Within(Tolerance));

        Assert.That(
            buffer7,
            Is.EqualTo(GetDenormalizedSamples([_samples6[3], .._samples7[..2]])).Within(Tolerance));

        Assert.That(
            buffer8,
            Is.EqualTo(GetDenormalizedSamples([.._samples7[2..], _samples8[0]])).Within(Tolerance));

        minuteEnumerableMock.VerifyAll();
        waveDataProviderMock.VerifyAll();
    }

    /// <summary>
    /// Test checks if ALL wave samples of every consecutive
    /// minute are saved to every consecutive buffer, sample by sample.
    /// <br/>Wave samples array sizes are BIGGER THAN buffer sizes.
    /// </summary>
    [Test]
    public void GenerateAudioFillsBuffersWithSamplesCorrectly3()
    {
        var waveDataProviderMock = GetWaveDataProviderMock();
        var minuteEnumerableMock = GetMinuteEnumerableMock();

        var waveGenerator = new WaveGeneratorTestable(
            _audioFormat,
            GetTimeProviderMock(_minute6, _minute6).Object,
            minuteEnumerableMock.Object,
            waveDataProviderMock.Object);

        var buffer6 = new float[5];
        var buffer7 = new float[5];

        waveGenerator.GenerateAudioExecute(buffer6);
        waveGenerator.GenerateAudioExecute(buffer7);

        Assert.That(
            buffer6,
            Is.EqualTo(GetDenormalizedSamples([.._samples6, .._samples7[..1]])).Within(Tolerance));

        Assert.That(
            buffer7,
            Is.EqualTo(GetDenormalizedSamples([.._samples7[1..], .._samples8[..2]])).Within(Tolerance));

        minuteEnumerableMock.VerifyAll();
        waveDataProviderMock.VerifyAll();
    }

    /// <summary>
    /// Test checks if the first minute wave data generation is repeated
    /// if it began before the turn of the minute and finished after.
    /// </summary>
    [Test]
    public void GenerateAudioRepeatsFirstMinuteGenerationIfNecessary()
    {
        var time = new DateTime(2000, 1, 1, 22, 06, 59, 999);
        DateTime[] times = [time, time.AddMilliseconds(10), time.AddMilliseconds(20), time.AddMilliseconds(30)];
        var timeProviderMock = GetTimeProviderMock(times);

        var waveDataProviderMock = GetWaveDataProviderMock(stringPassedMsFromMin7: true);
        var minuteEnumerableMock = new Mock<IMinuteEnumerable>();
        minuteEnumerableMock.SetupSequence(s => s.GetEnumerator())
            .Returns(GetMinutes1)
            .Returns(GetMinutes2);

        var waveGenerator = new WaveGeneratorTestable(
            _audioFormat,
            timeProviderMock.Object,
            minuteEnumerableMock.Object,
            waveDataProviderMock.Object);

        var buffer6 = new float[4];
        var buffer7 = new float[4];

        waveGenerator.GenerateAudioExecute(buffer6);
        waveGenerator.GenerateAudioExecute(buffer7);

        Assert.That(
            buffer6,
            Is.EqualTo(GetDenormalizedSamples(_samples7)).Within(Tolerance));

        Assert.That(
            buffer7,
            Is.EqualTo(GetDenormalizedSamples(_samples8)).Within(Tolerance));

        minuteEnumerableMock.Verify(v => v.GetEnumerator(), Times.Exactly(2));
        waveDataProviderMock.VerifyAll();
    }

    #region Helpers

    private static Mock<ITimeProvider> GetTimeProviderMock(params DateTime[] dates)
    {
        var timeProviderMock = new Mock<ITimeProvider>();

        var resultsSetup = timeProviderMock.SetupSequence(s => s.GetTime());

        foreach (var date in dates)
            resultsSetup.Returns(date);

        return timeProviderMock;
    }

    private Mock<IMinuteEnumerable> GetMinuteEnumerableMock()
    {
        var minuteEnumerableMock = new Mock<IMinuteEnumerable>();

        minuteEnumerableMock.Setup(x => x.GetEnumerator())
            .Returns(GetMinutes1)
            .Verifiable();

        return minuteEnumerableMock;
    }

    private IEnumerator<DateTime> GetMinutes1()
    {
        yield return _minute6;
        yield return _minute7;
        yield return _minute8;
    }

    private IEnumerator<DateTime> GetMinutes2()
    {
        yield return _minute7;
        yield return _minute8;
    }

    private Mock<IWaveDataProvider> GetWaveDataProviderMock(
        bool stringPassedMsFromMin6 = true,
        bool stringPassedMsFromMin7 = false,
        bool stringPassedMsFromMin8 = false)
    {
        var waveDataProviderMock = new Mock<IWaveDataProvider>();

        waveDataProviderMock.Setup(s => s.GetWaveData(_audioFormat.SampleRate, _minute6, stringPassedMsFromMin6))
            .Returns(_samples6)
            .Verifiable();

        waveDataProviderMock.Setup(s => s.GetWaveData(_audioFormat.SampleRate, _minute7, stringPassedMsFromMin7))
            .Returns(_samples7)
            .Verifiable();

        waveDataProviderMock.Setup(s => s.GetWaveData(_audioFormat.SampleRate, _minute8, stringPassedMsFromMin8))
            .Returns(_samples8)
            .Verifiable();

        return waveDataProviderMock;
    }

    private static IEnumerable<float> GetDenormalizedSamples(IEnumerable<short> samples) =>
        samples.Select(s => s / 32767f);

    private class WaveGeneratorTestable(
        AudioFormat format,
        ITimeProvider timeProvider,
        IMinuteEnumerable minutes,
        IWaveDataProvider waveDataProvider)
        : WaveGenerator(
            Mock.Of<AudioEngine>(),
            format,
            Mock.Of<ILogger<WaveGenerator>>(),
            timeProvider,
            minutes,
            waveDataProvider)
    {
        public void GenerateAudioExecute(Span<float> buffer) =>
            GenerateAudio(buffer, Format.Channels);
    }

    #endregion Helpers
}