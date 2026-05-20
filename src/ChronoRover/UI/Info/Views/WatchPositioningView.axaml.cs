using Avalonia.Controls;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ChronoRover.UI.Info.Views;

public partial class WatchPositioningView : ContentPage
{
    public WatchPositioningView()
    {
        InitializeComponent();
    }

    public WatchPositioningView(IServiceProvider serviceProvider) : this()
    {
        DataContext = serviceProvider.GetRequiredService<WatchPositioningViewModel>();
    }
}