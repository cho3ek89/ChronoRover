using Avalonia.Controls;

using ChronoRover.UI.Settings.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Settings.Views;

public partial class SignalTypeSelectView : ContentPage
{
    public SignalTypeSelectView()
    {
        InitializeComponent();
    }

    public SignalTypeSelectView(IServiceProvider serviceProvider) : this()
    {
        DataContext = serviceProvider.GetRequiredService<SignalTypeSelectViewModel>();
    }

    [SuppressMessage("ReSharper", "AsyncVoidEventHandlerMethod")]
    private async void SignalTypeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.RemovedItems.Count > 0)
            await Navigation!.PopAsync();
    }
}