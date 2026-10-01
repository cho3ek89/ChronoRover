using ChronoRover.Models;
using ChronoRover.Services.Settings;
using ChronoRover.UI.Settings.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Localization;

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

    public SignalTypeSelectViewModel(ISettingsManager settingsManager, IStringLocalizer<App> localizer)
    {
        _settingsManager = settingsManager;

        SignalTypes =
        [
            new SignalTypeListItem(SignalType.Dcf77, localizer["Country.Germany"], localizer["City.Mainflingen"]),
            new SignalTypeListItem(SignalType.Wwvb, localizer["Country.USA"], localizer["City.FortCollins"]),
            new SignalTypeListItem(SignalType.Jjy, localizer["Country.Japan"], localizer["City.TamuraSaga"]),
            new SignalTypeListItem(SignalType.Bpc, localizer["Country.China"], localizer["City.Shangqiu"]),
            new SignalTypeListItem(SignalType.Msf, localizer["Country.UnitedKingdom"], localizer["City.Anthorn"]),
        ];

        SelectedSignalType = SignalTypes
            .First(f => f.SignalType == _settingsManager.SignalType);
    }

    partial void OnSelectedSignalTypeChanged(SignalTypeListItem value)
    {
        _settingsManager.SignalType = value.SignalType;
    }
}