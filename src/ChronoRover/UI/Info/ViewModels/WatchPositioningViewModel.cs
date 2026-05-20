using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Info.ViewModels;

public partial class WatchPositioningViewModel : ObservableObject
{
    public WatchPositioningViewModel()
    {
        SelectedImageIndex = 0;

        var baseUri = new Uri("avares://ChronoRover/Assets/Images/");
        Images = new ReadOnlyCollection<Image>([
            LoadImage(new Uri(baseUri, "Manual01.jpg")),
            LoadImage(new Uri(baseUri, "Manual02.jpg")),
        ]);
    }

    [ObservableProperty]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
    public partial ReadOnlyCollection<Image> Images { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SelectNextImageCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectPreviousImageCommand))]
    public partial int SelectedImageIndex { get; set; }

    [RelayCommand(CanExecute = nameof(CanSelectNextImage))]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void SelectNextImage()
    {
        SelectedImageIndex = +1;
    }

    [RelayCommand(CanExecute = nameof(CanSelectPreviousImage))]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void SelectPreviousImage()
    {
        SelectedImageIndex = -1;
    }

    private bool CanSelectNextImage() => SelectedImageIndex < Images.Count - 1;

    private bool CanSelectPreviousImage() => SelectedImageIndex > 0;

    private static Image LoadImage(Uri uri) => new()
    {
        Source = new Bitmap(AssetLoader.Open(uri)),
    };
}