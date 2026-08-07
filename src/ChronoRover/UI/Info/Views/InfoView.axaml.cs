using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

using ChronoRover.UI.Info.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

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
        await NavigateToView<ManualView>();
    }

    private async void ManualKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            await NavigateToView<ManualView>();
    }

    private async void AboutClick(object sender, RoutedEventArgs e)
    {
        await NavigateToView<AboutView>();
    }

    private async void AboutKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            await NavigateToView<AboutView>();
    }

    private async Task NavigateToView<T>() where T : ContentPage
    {
        var view = _serviceProvider.GetRequiredService<T>();
        await Navigation!.PushAsync(view);
    }
}