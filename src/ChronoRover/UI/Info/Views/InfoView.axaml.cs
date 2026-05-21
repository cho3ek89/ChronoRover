using Avalonia.Controls;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Info.Views;

[SuppressMessage("ReSharper", "AsyncVoidEventHandlerMethod")]
public partial class InfoView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public InfoView()
    {
        InitializeComponent();
    }

    public InfoView(IServiceProvider serviceProvider) : this()
    {
        _serviceProvider = serviceProvider;

        DataContext = _serviceProvider.GetRequiredService<InfoViewModel>();
    }

    private async void ManualClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<ManualView>();
        await Navigation!.PushAsync(view);
    }

    private async void AboutClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<AboutView>();
        await Navigation!.PushAsync(view);
    }
}