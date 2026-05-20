using ChronoRover.Models;
using ChronoRover.Providers.TimeZone;

using SoundFlow.Structs;

namespace ChronoRover.Providers.Settings;

public class DefaultSettingsProvider(
    ITimeZoneProvider timeZoneProvider) : ISettingsProvider
{
    public AudioFormat GetAudioFormat()
    {
        var audioFormat = AudioFormat.Broadcast;
        audioFormat.SampleRate = 44100;

        return audioFormat;
    }

    public SignalType GetSignalType()
    {
        var timeZone = timeZoneProvider.GetLocalTimeZone();
        var offset = timeZone.BaseUtcOffset.TotalHours;

        return offset switch
        {
            < -1 => SignalType.Wwvb,
            < 1 => SignalType.Msf,
            < 4 => SignalType.Dcf77,
            < 9 => SignalType.Bpc,
            _ => SignalType.Jjy
        };
    }
}