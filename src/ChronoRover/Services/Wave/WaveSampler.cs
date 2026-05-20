using SoundFlow.Abstracts;
using SoundFlow.Structs;

using System;

namespace ChronoRover.Services.Wave;

public class WaveSampler(
    AudioFormat format,
    Action<float> onSampleReady) : AudioAnalyzer(format)
{
    protected override void Analyze(ReadOnlySpan<float> buffer, int channels)
    {
        onSampleReady(buffer[^1]);
    }
}