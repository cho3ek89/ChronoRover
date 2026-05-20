using SoundFlow.Structs;
using SoundFlow.Visualization;

using System;

namespace ChronoRover.Services.Wave;

public class WaveLevelExtractor(
    AudioFormat format,
    Action<float, float> onDataReady) : LevelMeterAnalyzer(format)
{
    protected override void Analyze(ReadOnlySpan<float> buffer, int channels)
    {
        base.Analyze(buffer, channels);

        onDataReady(Peak, Rms);
    }
}