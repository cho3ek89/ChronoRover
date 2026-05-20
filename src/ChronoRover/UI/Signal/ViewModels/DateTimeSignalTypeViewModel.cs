using CommunityToolkit.Mvvm.ComponentModel;

namespace ChronoRover.UI.Signal.ViewModels;

public partial class DateTimeSignalTypeViewModel : ObservableObject
{
    [ObservableProperty]
    public partial DateTimeViewModel DateTimeViewModel { get; private set; }

    [ObservableProperty]
    public partial SignalTypeViewModel SignalTypeViewModel { get; private set; }

    public DateTimeSignalTypeViewModel(
        DateTimeViewModel dateTimeViewModel,
        SignalTypeViewModel signalTypeViewModel)
    {
        DateTimeViewModel = dateTimeViewModel;
        SignalTypeViewModel = signalTypeViewModel;
    }
}