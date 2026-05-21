using Avalonia.Controls;

using ChronoRover.UI.Dialogs.ViewModels;
using ChronoRover.UI.Dialogs.Views;
using ChronoRover.UI.Models.Messages;
using ChronoRover.UI.Shell.ViewModels;
using ChronoRover.UI.Signal.Views;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics.CodeAnalysis;

namespace ChronoRover.UI.Shell.Views;

[SuppressMessage("ReSharper", "AsyncVoidMethod")]
public partial class MainView : UserControl
{
    private readonly IServiceProvider _serviceProvider;

    public MainView()
    {
        InitializeComponent();
    }

    public MainView(IServiceProvider serviceProvider) : this()
    {
        _serviceProvider = serviceProvider;

        SubscribeForErrors();

        DataContext = _serviceProvider.GetRequiredService<MainViewModel>();

        Navigator.PushAsync(_serviceProvider.GetRequiredService<SignalView>())
            .GetAwaiter().GetResult();
    }

    private void SubscribeForErrors()
    {
        var messenger = _serviceProvider.GetRequiredService<IMessenger>();

        messenger.Register<ErrorMessage>(this, async void (_, error) =>
        {
            var errorView = _serviceProvider.GetRequiredService<ErrorView>();
            var errorViewModel = errorView.DataContext as ErrorViewModel;

            var (title, ex) = error;
            errorViewModel!.Title = title;
            errorViewModel!.Exception = ex;

            await Navigator.PushAsync(errorView);
        });
    }
}