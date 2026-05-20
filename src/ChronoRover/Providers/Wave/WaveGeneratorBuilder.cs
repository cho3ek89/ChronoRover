using ChronoRover.Models;
using ChronoRover.Providers.Time;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Services.Settings;
using ChronoRover.Services.Wave;

using Microsoft.Extensions.Logging;

using SoundFlow.Abstracts;
using SoundFlow.Structs;

using System;

namespace ChronoRover.Providers.Wave;

public class WaveGeneratorBuilder(
    AudioEngine engine,
    ILogger<WaveGenerator> logger,
    ITimeProvider timeProvider,
    IMinuteEnumerableFactory minuteEnumerableFactory,
    IWaveDataProviderFactory waveDataProviderFactory) : IWaveGeneratorBuilder
{
    private AudioFormat _format;

    private SignalType _signalType;

    public IWaveGeneratorBuilder InitFromSettings(ISettingsManager settingsManager)
    {
        _format = settingsManager.AudioFormat;
        _signalType = settingsManager.SignalType;
        return this;
    }

    public IWaveGeneratorBuilder WithAudioFormat(AudioFormat format)
    {
        _format = format;
        return this;
    }

    public IWaveGeneratorBuilder WithSignalType(SignalType signalType)
    {
        _signalType = signalType;
        return this;
    }

    public WaveGenerator Build()
    {
        ArgumentNullException.ThrowIfNull(engine);
        ArgumentNullException.ThrowIfNull(timeProvider);
        ArgumentNullException.ThrowIfNull(minuteEnumerableFactory);
        ArgumentNullException.ThrowIfNull(waveDataProviderFactory);

        var waveDateProvider = waveDataProviderFactory.GetWaveDateProvider(_signalType);
        var minuteEnumerable = minuteEnumerableFactory.GetMinuteEnumerable(_signalType);

        var waveGenerator = new WaveGenerator(
            engine,
            _format,
            logger,
            timeProvider,
            minuteEnumerable,
            waveDateProvider);

        return waveGenerator;
    }
}