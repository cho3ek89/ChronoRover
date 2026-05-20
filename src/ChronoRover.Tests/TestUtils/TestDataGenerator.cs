using System;

namespace ChronoRover.Tests.TestUtils;

public static class TestDataGenerator
{
    public static float[] GetSineWaveTestData(int sampleRate = 41_000, int frequency = 15_000)
    {
        var samples = new float[sampleRate];

        for (var i = 0; i < sampleRate; i++)
        {
            var t = (float)i / sampleRate;
            var sample = MathF.Sin(t * frequency * MathF.PI * 2);

            samples[i] = sample;
        }

        return samples;
    }
}