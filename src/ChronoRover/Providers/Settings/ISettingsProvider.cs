using ChronoRover.Models;

using SoundFlow.Structs;

namespace ChronoRover.Providers.Settings;

public interface ISettingsProvider
{
    AudioFormat GetAudioFormat();

    SignalType GetSignalType();
}