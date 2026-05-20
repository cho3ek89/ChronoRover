using ChronoRover.Models;
using ChronoRover.Services.Settings;
using ChronoRover.UI.Settings.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ChronoRover.UI.Settings.ViewModels;

public partial class SignalTypeSelectViewModel : ObservableObject
{
    private readonly ISettingsManager _settingsManager;

    [ObservableProperty]
    public partial SignalTypeListItem SelectedSignalType { get; set; }

    [ObservableProperty]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
    public partial IReadOnlyCollection<SignalTypeListItem> SignalTypes { get; private set; }

    public SignalTypeSelectViewModel(ISettingsManager settingsManager)
    {
        _settingsManager = settingsManager;

        SignalTypes =
        [
            new SignalTypeListItem(SignalType.Dcf77, "Germany", "Mainflingen"),
            new SignalTypeListItem(SignalType.Wwvb, "USA", "Fort Collins"),
            new SignalTypeListItem(SignalType.Jjy, "Japan", "Tamura/Saga"),
            new SignalTypeListItem(SignalType.Bpc, "China", "Shangqiu"),
            new SignalTypeListItem(SignalType.Msf, "United Kingdom", "Anthorn"),
        ];

        SelectedSignalType = SignalTypes
            .First(f => f.SignalType == _settingsManager.SignalType);
    }

    partial void OnSelectedSignalTypeChanged(SignalTypeListItem value)
    {
        _settingsManager.SignalType = value.SignalType;
    }
}