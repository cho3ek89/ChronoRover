using ChronoRover.Models;
using ChronoRover.Providers.Signal;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ChronoRover.Providers.Wave;

public class MsfWaveDataProvider(
    ISignalProviderFactory signalProviderFactory) : WaveDataProviderBase(signalProviderFactory, SignalType.Msf)
{
    private const float Frequency = 15000; // 60 kHz / 4

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public override short[] GetWaveData(int sampleRate, DateTime dateTime, bool stripPassedMs = false)
    {
        var values = SignalProvider.GetMinuteSignal(dateTime);

        var initSec = stripPassedMs ? dateTime.Second : 0;
        var initSample = stripPassedMs ? sampleRate * dateTime.Millisecond / 1000 : 0;

        var samples = new short[(sampleRate - initSample) + sampleRate * (60 - initSec - 1)];
        var j = 0;

        for (var s = initSec; s < 60; s++)
        {
            var aBit = GetABit(values, s);
            var bBit = GetBBit(values, s);

            for (var i = initSample; i < sampleRate; i++)
            {
                var ampFactor = 1.0;
                var secProgress = (double)i / sampleRate;

                if (s == 0)
                {
                    if (secProgress < 0.5)
                        ampFactor = 0;
                }
                else
                {
                    if (secProgress < 0.1)
                    {
                        ampFactor = 0;
                    }
                    else if (secProgress < 0.2)
                    {
                        if (aBit) ampFactor = 0;
                    }
                    else if (secProgress < 0.3)
                    {
                        if (bBit) ampFactor = 0;
                    }
                }

                var carrier = Math.Sin(secProgress * Frequency * Math.PI * 2);
                var sample = (short)(carrier * ampFactor * short.MaxValue);

                samples[j++] = sample;
            }

            initSample = 0;
        }

        return samples;
    }

    private static bool GetABit(ReadOnlySpan<bool> values, int second) => second switch
    {
        0 => true,
        < 17 => false,
        < 52 => values[second],
        52 => false,
        < 59 => true,
        59 => false,
        _ => throw new ArgumentOutOfRangeException(nameof(second))
    };

    private static bool GetBBit(ReadOnlySpan<bool> values, int second) => second switch
    {
        0 => true,
        < 17 => values[second],
        < 52 => false,
        52 => false,
        < 59 => values[second],
        59 => false,
        _ => throw new ArgumentOutOfRangeException(nameof(second))
    };
}