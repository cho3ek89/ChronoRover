using Avalonia.Controls;

using ChronoRover.UI.Dialogs.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace ChronoRover.UI.Dialogs.Views;

public partial class ErrorView : ContentPage
{
    public ErrorView()
    {
        InitializeComponent();
    }

    public ErrorView(IServiceProvider serviceProvider) : this()
    {
        DataContext = serviceProvider.GetRequiredService<ErrorViewModel>();
    }
}