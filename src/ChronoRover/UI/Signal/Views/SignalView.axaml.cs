using Avalonia.Controls;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.Views;
using ChronoRover.UI.Settings.Views;
using ChronoRover.UI.Signal.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ChronoRover.UI.Signal.Views;

public partial class SignalView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public SignalView()
    {
        InitializeComponent();
    }

    public SignalView(IServiceProvider serviceProvider) : this()
    {
        _serviceProvider = serviceProvider;

        DataContext = _serviceProvider.GetRequiredService<SignalViewModel>();
    }

    private void SignalTypeSelectButtonClicked(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<SignalTypeSelectView>();
        Navigation!.PushAsync(view);
    }

    private void InfoButtonClicked(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<InfoView>();
        Navigation!.PushAsync(view);
    }
}