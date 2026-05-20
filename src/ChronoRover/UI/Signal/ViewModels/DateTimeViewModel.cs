using ChronoRover.Providers.Time;
using ChronoRover.UI.Models.Messages;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Logging;

using System;
using System.Threading;

namespace ChronoRover.UI.Signal.ViewModels;

public partial class DateTimeViewModel : ObservableObject, IDisposable
{
    private readonly ITimeProvider _timeProvider;

    private readonly ILogger<DateTimeViewModel> _logger;

    private readonly Timer _timer;

    [ObservableProperty]
    public partial DateTime DateTime { get; private set; }

    public DateTimeViewModel(
        ITimeProvider timeProvider,
        IMessenger messenger,
        ILogger<DateTimeViewModel> logger)
    {
        _timeProvider = timeProvider;
        _logger = logger;

        DateTime = GetTime();

        _timer = new Timer(
            _ => UpdateTime(),
            null,
            GetTimersDueTime(),
            1000);

        messenger.Register<AppActivatedMessage>(this, (_, _) =>
        {
            // When the Android application is deactivated, the timer's thread may be suspended.
            // It needs to be rescheduled when an application is reactivated.

            RescheduleTimer();
        });
    }

    private DateTime GetTime() => _timeProvider.GetTime();

    private void UpdateTime()
    {
        DateTime = GetTime();

        if (_logger.IsEnabled(LogLevel.Trace))
            _logger.Log(LogLevel.Trace, "{DateTime:HH:mm:ss:fff}", DateTime);
    }

    private int GetTimersDueTime() => 1000 - DateTime.Millisecond + 2;

    private void RescheduleTimer()
    {
        UpdateTime();

        _timer?.Change(GetTimersDueTime(), 1000);

        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.Log(LogLevel.Debug, "Timer has been rescheduled.");
    }

    public void Dispose()
    {
        _timer?.Dispose();

        GC.SuppressFinalize(this);
    }
}