using ChronoRover.Models;
using ChronoRover.Providers.Signal;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Wave;

public class BpcWaveDataProvider(
    ISignalProviderFactory signalProviderFactory) : WaveDataProviderBase(signalProviderFactory, SignalType.Bpc)
{
    private const float Frequency = 13700; // 68.5 kHz / 5

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override short[] GetWaveData(int sampleRate, DateTime dateTime, bool stripPassedMs = false)
    {
        var values = SignalProvider.GetMinuteSignal(dateTime);
        // HACK: Moving marker to the back. It won't work without it - investigate why.
        values = [..values[2..], ..values[..2]];

        var initSec = stripPassedMs ? dateTime.Second : 0;
        var initSample = stripPassedMs ? sampleRate * dateTime.Millisecond / 1000 : 0;

        var samples = new short[(sampleRate - initSample) + sampleRate * (60 - initSec - 1)];
        var j = 0;

        for (var s = initSec; s < 60; s++)
        {
            var msBit = values[s << 1];
            var lsBit = values[(s << 1) + 1];
            var redAmpLength = GetReducedAmplitudeLength(msBit, lsBit);

            for (var i = initSample; i < sampleRate; i++)
            {
                var ampFactor = 1.0;
                var secProgress = (double)i / sampleRate;

                if (s != 19 && s != 39 && s != 59)
                //if (s != 0 && s != 20 && s != 40)
                {
                    if (secProgress < redAmpLength) ampFactor = 0.1;
                }

                var carrier = Math.Sin(secProgress * Frequency * Math.PI * 2);
                var sample = (short)(carrier * ampFactor * short.MaxValue);

                samples[j++] = sample;
            }

            initSample = 0;
        }

        return samples;
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    private static float GetReducedAmplitudeLength(bool msBit, bool lsBit)
    {
        if (msBit && lsBit)
            return 0.4f;

        if (!msBit && !lsBit)
            return 0.1f;

        return msBit ? 0.3f : 0.2f;
    }
}