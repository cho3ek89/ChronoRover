using ChronoRover.Models;
using ChronoRover.Services.Settings;
using ChronoRover.Services.Wave;

using SoundFlow.Structs;

namespace ChronoRover.Providers.Wave;

public interface IWaveGeneratorBuilder
{
    IWaveGeneratorBuilder InitFromSettings(ISettingsManager settingsManager);

    IWaveGeneratorBuilder WithAudioFormat(AudioFormat format);

    IWaveGeneratorBuilder WithSignalType(SignalType signalType);

    WaveGenerator Build();
}