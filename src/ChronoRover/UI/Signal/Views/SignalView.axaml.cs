using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

using ChronoRover.UI.Info.Views;
using ChronoRover.UI.Settings.Views;
using ChronoRover.UI.Signal.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Signal.Views;

[SuppressMessage("ReSharper", "AsyncVoidEventHandlerMethod")]
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

    private async void SignalTypeSelectButtonClicked(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<SignalTypeSelectView>();
        await Navigation!.PushAsync(view);
    }

    private async void InfoButtonClicked(object sender, RoutedEventArgs e)
    {
        var view = _serviceProvider.GetRequiredService<InfoView>();
        await Navigation!.PushAsync(view);
    }

    private void PlayButtonPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != IsEffectivelyEnabledProperty)
            return;

        var isPlayEnabled = (bool)e.NewValue!;

        XYFocus.SetDown(
            SignalTypeSelectButton,
            isPlayEnabled ? PlayButton : StopButton);

        XYFocus.SetDown(
            InfoButton,
            isPlayEnabled ? PlayButton : StopButton);
    }
}