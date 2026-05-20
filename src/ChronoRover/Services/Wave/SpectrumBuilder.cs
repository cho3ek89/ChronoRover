using SoundFlow.Structs;
using SoundFlow.Visualization;

using System;

namespace ChronoRover.Services.Wave;

public class SpectrumBuilder(
    AudioFormat format,
    int fftSize,
    Action<float[]> onDataReady) : SpectrumAnalyzer(format, fftSize)
{
    protected override void Analyze(ReadOnlySpan<float> buffer, int channels)
    {
        base.Analyze(buffer, channels);

        onDataReady(SpectrumData);
    }
}