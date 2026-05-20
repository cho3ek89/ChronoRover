using Avalonia.Controls;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ChronoRover.UI.Info.Views;

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

    private void ManualClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<ManualView>();
        Navigation!.PushAsync(view);
    }

    private void AboutClick(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<AboutView>();
        Navigation!.PushAsync(view);
    }
}