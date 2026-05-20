using CommunityToolkit.Mvvm.ComponentModel;

namespace ChronoRover.UI.Signal.ViewModels;

public partial class LevelsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial float Peak { get; set; }

    public void ClearValues()
    {
        Peak = 0;
    }
}