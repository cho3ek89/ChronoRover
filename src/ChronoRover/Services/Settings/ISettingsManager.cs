using ChronoRover.Models;

using SoundFlow.Structs;

using System;

namespace ChronoRover.Services.Settings;

public interface ISettingsManager
{
    SignalType SignalType { get; set; }

    AudioFormat AudioFormat { get; }

    event EventHandler OnSignalTypeChanged;
}