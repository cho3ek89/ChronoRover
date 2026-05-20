using ChronoRover.Models;
using ChronoRover.Services.Settings;

using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Signal.ViewModels;

public partial class SignalTypeViewModel : ObservableObject
{
    private readonly ISettingsManager _settingsManager;

    [ObservableProperty]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public partial SignalType SignalType { get; private set; }

    public SignalTypeViewModel(ISettingsManager settingsManager)
    {
        _settingsManager = settingsManager;

        SignalType = _settingsManager.SignalType;

        settingsManager.OnSignalTypeChanged += SetSignalType;
    }

    private void SetSignalType(object sender, EventArgs e)
    {
        SignalType = _settingsManager.SignalType;
    }
}