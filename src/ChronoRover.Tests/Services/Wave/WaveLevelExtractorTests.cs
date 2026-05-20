using ChronoRover.Services.Wave;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using SoundFlow.Structs;

using System;
using System.Linq;

namespace ChronoRover.Tests.Services.Wave;

public class WaveLevelExtractorTests
{
    [Test]
    public void AnalyzeInvokesOnDataReadyWithCorrectLevelMeters()
    {
        var samples = TestDataGenerator.GetSineWaveTestData().ToArray();

        var invoked = false;

        var waveSamples = new WaveLevelExtractor(
            AudioFormat.Broadcast,
            (actualPeak, actualRms) =>
            {
                Assert.That(actualPeak, Is.EqualTo(0.9995).Within(0.0001));
                Assert.That(actualRms, Is.EqualTo(0.7071).Within(0.0001));
                invoked = true;
            });

        waveSamples.Process(samples.AsSpan(), 1);

        Assert.That(invoked, Is.True);
    }
}