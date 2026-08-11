using Avalonia.Controls;
using Avalonia.Interactivity;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Info.Views;

public partial class AboutView : ContentPage
{
    public AboutView()
    {
        InitializeComponent();
    }

    [SuppressMessage("ReSharper", "AsyncVoidEventHandlerMethod")]
    private async void GitHubClick(object sender, RoutedEventArgs e)
    {
        var launcher = TopLevel.GetTopLevel(this)!.Launcher;
        var uri = new Uri("https://github.com/cho3ek89/ChronoRover");

        await launcher.LaunchUriAsync(uri);
    }
}