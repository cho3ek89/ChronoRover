using CommunityToolkit.Mvvm.ComponentModel;

using System;

namespace ChronoRover.UI.Dialogs.ViewModels;

public partial class ErrorViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Title { get; set; }

    [ObservableProperty]
    public partial Exception Exception { get; set; }
}