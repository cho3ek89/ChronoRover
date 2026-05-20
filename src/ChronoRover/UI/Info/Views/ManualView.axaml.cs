using Avalonia.Controls;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ChronoRover.UI.Info.Views;

public partial class ManualView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public ManualView()
    {
        InitializeComponent();
    }

    public ManualView(IServiceProvider serviceProvider) : this()
    {
        _serviceProvider = serviceProvider;

        DataContext = _serviceProvider.GetRequiredService<InfoViewModel>();
    }

    private void WatchPositioningPicturesClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<WatchPositioningView>();
        Navigation!.PushAsync(view);
    }
}