using ChronoRover.Services.Wave;
using ChronoRover.Tests.TestUtils;

using NUnit.Framework;

using SoundFlow.Structs;

using System;
using System.Linq;

namespace ChronoRover.Tests.Services.Wave;

public class WaveSamplerTests
{
    [Test]
    public void AnalyzeInvokesOnSampleReadyWithLastBufferSample()
    {
        var samples = TestDataGenerator.GetSineWaveTestData().ToArray();

        var invoked = false;

        var waveSamples = new WaveSampler(
            AudioFormat.Broadcast,
            actualSample =>
            {
                Assert.That(actualSample, Is.EqualTo(-0.7488).Within(0.0001));
                invoked = true;
            });

        waveSamples.Process(samples.AsSpan(), 1);

        Assert.That(invoked, Is.True);
    }
}