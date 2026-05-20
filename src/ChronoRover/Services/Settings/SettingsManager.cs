using ChronoRover.Models;
using ChronoRover.Providers.Settings;

using SoundFlow.Structs;

using System;
using System.Threading;

namespace ChronoRover.Services.Settings;

public class SettingsManager(ISettingsProvider settingsProvider) : ISettingsManager
{
    public SignalType SignalType
    {
        get => Interlocked.CompareExchange(ref field, default, default);
        set
        {
            Interlocked.Exchange(ref field, value);
            OnSignalTypeChanged?.Invoke(this, EventArgs.Empty);
        }
    } = settingsProvider.GetSignalType();

    public AudioFormat AudioFormat { get; } = settingsProvider.GetAudioFormat();

    public event EventHandler OnSignalTypeChanged;
}