using ChronoRover.Providers.Time;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Providers.Wave;

using Microsoft.Extensions.Logging;

using SoundFlow.Abstracts;
using SoundFlow.Structs;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChronoRover.Services.Wave;

public class WaveGenerator(
    AudioEngine engine,
    AudioFormat format,
    ILogger<WaveGenerator> logger,
    ITimeProvider timeProvider,
    IMinuteEnumerable minutes,
    IWaveDataProvider waveDataProvider) : SoundComponent(engine, format)
{
    private IEnumerator<DateTime> _iterMinutes;

    private short[] _waveData;

    private IEnumerator _iter;

    private Task<short[]> _getWaveDataTask;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void GenerateAudio(Span<float> buffer, int channels)
    {
        if (_iter == null)
            Initialize();

        var i = 0;
        while (i < buffer.Length)
        {
            if (_iter!.MoveNext())
            {
                var val = (short)_iter.Current!;
                buffer[i++] = val / 32767f;
            }
            else
            {
                LogNow();

                _getWaveDataTask.Wait();
                _waveData = _getWaveDataTask.Result;
                _iter = _waveData.GetEnumerator();

                // Wave data for the next minutes is generated asynchronously.
                _iterMinutes.MoveNext();
                _getWaveDataTask = Task.Run(() => GetWaveData(false));
            }
        }
    }

    private void LogNow()
    {
        if (logger.IsEnabled(LogLevel.Information))
            logger.Log(LogLevel.Information, "Generating samples at: {GetTime:HH:mm:ss:fff}", timeProvider.GetTime());
    }

    private void Initialize()
    {
        logger.LogInformation("Staring...");

        DateTime initStartTime;
        DateTime initEndTime;

        // Generating wave data for the first minute.
        do
        {
            LogNow();

            _iterMinutes = minutes.GetEnumerator();
            _iterMinutes.MoveNext();

            initStartTime = timeProvider.GetTime();
            _waveData = GetWaveData(true);
            initEndTime = timeProvider.GetTime();

            // Wave data generation for the first minute needs to be repeated
            // if it began before the turn of the minute and finished after.
        } while (initStartTime.Minute != initEndTime.Minute);

        // Cutting off the amount of samples that would have been passed
        // to the buffer during the time it took to generate wave data.
        var initDuration = (initEndTime - initStartTime).Milliseconds;
        var samplesToCut = initDuration * Format.SampleRate / 1000;
        _iter = _waveData.GetEnumerator();
        for (var i = 0; i < samplesToCut; i++)
            _iter.MoveNext();

        // Wave data for the next minutes is generated asynchronously.
        _iterMinutes.MoveNext();
        _getWaveDataTask = Task.Run(() => GetWaveData(false));
    }

    private short[] GetWaveData(bool isInitSecond) =>
        waveDataProvider.GetWaveData(Format.SampleRate, _iterMinutes.Current, isInitSecond);
}