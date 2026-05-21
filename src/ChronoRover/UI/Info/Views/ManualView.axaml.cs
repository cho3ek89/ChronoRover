using Avalonia.Controls;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Info.Views;

[SuppressMessage("ReSharper", "AsyncVoidEventHandlerMethod")]
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

    private async void WatchPositioningPicturesClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<WatchPositioningView>();
        await Navigation!.PushAsync(view);
    }
}