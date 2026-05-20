using ChronoRover.Services.Wave;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using SoundFlow.Structs;

using System;
using System.Linq;

namespace ChronoRover.Tests.Services.Wave;

public class SpectrumBuilderTests
{
    [Test]
    public void AnalyzeInvokesOnDataReadyWithCorrectSpectrumData()
    {
        var samples = TestDataGenerator.GetSineWaveTestData().ToArray();

        var invoked = false;

        var expectedSpectrumData = new[]
        {
            0.0592f, 0.0595f, 0.0604f, 0.0619f, 0.0640f, 0.0666f, 0.0693f, 0.0705f,
            0.0640f, 0.0303f, 0.4897f, 5.6966f, 7.8517f, 2.0461f, 0.0677f, 0.0544f,
        };

        var spectrumBuilder = new SpectrumBuilder(
            AudioFormat.Broadcast,
            16 * 2, // 16 bars
            actualSpectrumData =>
            {
                Assert.That(actualSpectrumData, Is.EqualTo(expectedSpectrumData).Within(0.0001));
                invoked = true;
            });

        spectrumBuilder.Process(samples.AsSpan(), 1);

        Assert.That(invoked, Is.True);
    }
}