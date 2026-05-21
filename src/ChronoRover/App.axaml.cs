using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ChronoRover.Providers.Settings;
using ChronoRover.Providers.Signal;
using ChronoRover.Providers.Time;
using ChronoRover.Providers.Time.TimeWindow;
using ChronoRover.Providers.TimeZone;
using ChronoRover.Providers.Wave;
using ChronoRover.Services.Settings;
using ChronoRover.UI.Dialogs.ViewModels;
using ChronoRover.UI.Dialogs.Views;
using ChronoRover.UI.Info.ViewModels;
using ChronoRover.UI.Info.Views;
using ChronoRover.UI.Models.Messages;
using ChronoRover.UI.Settings.ViewModels;
using ChronoRover.UI.Settings.Views;
using ChronoRover.UI.Shell.ViewModels;
using ChronoRover.UI.Shell.Views;
using ChronoRover.UI.Signal.ViewModels;
using ChronoRover.UI.Signal.Views;

using CommunityToolkit.Mvvm.Messaging;

using GuerrillaNtp;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SoundFlow.Abstracts;
using SoundFlow.Backends.MiniAudio;

using System;
using System.Threading.Tasks;

namespace ChronoRover;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        RegisterServices(serviceCollection);
        RegisterViewModels(serviceCollection);
        RegisterViews(serviceCollection);
        var services = serviceCollection.BuildServiceProvider();

        ConfigureExceptionHandling(services);

        var mainView = services.GetRequiredService<MainView>();

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow { Content = mainView };
                break;
            case IActivityApplicationLifetime singleViewFactoryApplicationLifetime:
                singleViewFactoryApplicationLifetime.MainViewFactory = () => mainView;
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = mainView;
                break;
        }

        SubscribeToEvents(services);

        base.OnFrameworkInitializationCompleted();
    }

    private static void ConfigureExceptionHandling(IServiceProvider services)
    {
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            args.SetObserved();
            LogException(args.Exception);
        };

        Avalonia.Threading.Dispatcher.UIThread.UnhandledException += (_, args) =>
        {
            args.Handled = true;
            LogException(args.Exception);
        };

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var ex = args.ExceptionObject as Exception;
            LogException(ex, LogLevel.Critical);
        };

        return;

        void LogException(Exception ex, LogLevel logLevel = LogLevel.Error)
        {
            var logger = services.GetService<ILogger<App>>();
            logger?.Log(logLevel, ex, "An error occurred during application execution!");
        }
    }

    private void SubscribeToEvents(IServiceProvider services)
    {
        if (this.TryGetFeature<IActivatableLifetime>() is not { } lifetime) return;

        lifetime.Activated += (_, args) =>
        {
            if (args.Kind != ActivationKind.Background) return;

            var messenger = services.GetService<IMessenger>();
            messenger?.Send<AppActivatedMessage>();
        };
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = false;
                options.SingleLine = true;
            });
        });

        services.AddSingleton<IMessenger, WeakReferenceMessenger>();

        services.AddTransient<ISettingsProvider, DefaultSettingsProvider>();
        services.AddSingleton<ISettingsManager, SettingsManager>();

        // It has to be added from factory lambda, otherwise it crashes Native AOT execution!
        services.AddSingleton<AudioEngine, MiniAudioEngine>(_ => new MiniAudioEngine());

        services.AddSingleton(serviceProvider =>
        {
            try
            {
                var client = NtpClient.Default;
                return client.Query();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetService<ILogger<App>>();
                logger?.LogWarning(
                    ex,
                    "An error occurred while retrieving the time from the NTP server. Falling back to system time.");

                return NtpClock.LocalFallback;
            }
        });

        services.AddSingleton<ITimeProvider, Providers.Time.TimeProvider>();

        services.AddTransient<ITimeZoneProvider>(_ =>
            OperatingSystem.IsWindows() ? new TimeZoneProviderWin() : new TimeZoneProvider());

        services.AddTransient<IMinuteEnumerableFactory, MinuteEnumerableFactory>();
        services.AddTransient<ISignalProviderFactory, SignalProviderFactory>();
        services.AddTransient<IWaveDataProviderFactory, WaveDataProviderFactory>();
        services.AddTransient<IWaveGeneratorBuilder, WaveGeneratorBuilder>();
    }

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();

        services.AddTransient<AboutViewModel>();
        services.AddTransient<InfoViewModel>();
        services.AddTransient<ManualViewModel>();
        services.AddTransient<WatchPositioningViewModel>();

        services.AddTransient<SignalTypeSelectViewModel>();

        services.AddSingleton<DateTimeSignalTypeViewModel>();
        services.AddSingleton<DateTimeViewModel>();
        services.AddSingleton<LevelsViewModel>();
        services.AddSingleton<SignalTypeViewModel>();
        services.AddSingleton<SignalViewModel>();
        services.AddSingleton<SpectrumChartViewModel>();
        services.AddSingleton<WaveChartViewModel>();

        services.AddTransient<ErrorViewModel>();
    }

    private static void RegisterViews(IServiceCollection services)
    {
        services.AddSingleton<MainView>();

        services.AddTransient<AboutView>();
        services.AddTransient<InfoView>();
        services.AddTransient<ManualView>();
        services.AddTransient<WatchPositioningView>();

        services.AddTransient<SignalTypeSelectView>();

        services.AddSingleton<SignalView>();

        services.AddTransient<ErrorView>();
    }
}