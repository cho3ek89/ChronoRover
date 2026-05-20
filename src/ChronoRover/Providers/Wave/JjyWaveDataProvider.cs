using ChronoRover.Models;
using ChronoRover.Providers.Signal;

using System;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Wave;

public class JjyWaveDataProvider(
    ISignalProviderFactory signalProviderFactory) : WaveDataProviderBase(signalProviderFactory, SignalType.Jjy)
{
    private const float Frequency = 13333.33f; // 40 kHz / 3

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override short[] GetWaveData(int sampleRate, DateTime dateTime, bool stripPassedMs = false)
    {
        var values = SignalProvider.GetMinuteSignal(dateTime);

        var initSec = stripPassedMs ? dateTime.Second : 0;
        var initSample = stripPassedMs ? sampleRate * dateTime.Millisecond / 1000 : 0;

        var samples = new short[(sampleRate - initSample) + sampleRate * (60 - initSec - 1)];
        var j = 0;

        for (var s = initSec; s < 60; s++)
        {
            for (var i = initSample; i < sampleRate; i++)
            {
                var ampFactor = 1.0;
                var secProgress = (double)i / sampleRate;

                if (s != 0 && s != 9 && s != 19 && s != 29 && s != 39 && s != 49 && s != 59)
                {
                    if (values[s])
                    {
                        if (secProgress > 0.5) ampFactor = 0.1;
                    }
                    else
                    {
                        if (secProgress > 0.8) ampFactor = 0.1;
                    }
                }
                else
                {
                    if (secProgress > 0.2) ampFactor = 0.1;
                }

                var carrier = Math.Sin(secProgress * Frequency * Math.PI * 2);
                var sample = (short)(carrier * ampFactor * short.MaxValue);

                samples[j++] = sample;
            }

            initSample = 0;
        }

        return samples;
    }
}