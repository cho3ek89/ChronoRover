using ChronoRover.Providers.Wave;
using ChronoRover.Services.Settings;
using ChronoRover.Services.Wave;
using ChronoRover.UI.Models.Messages;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Logging;

using SoundFlow.Abstracts;
using SoundFlow.Abstracts.Devices;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ChronoRover.UI.Signal.ViewModels;

public partial class SignalViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PlayCommand))]
    [NotifyCanExecuteChangedFor(nameof(StopCommand))]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public partial bool IsPlaying { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PlayCommand))]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public partial bool IsInitializing { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StopCommand))]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public partial bool IsFinalizing { get; private set; }

    [ObservableProperty]
    public partial DateTimeSignalTypeViewModel DateTimeSignalTypeViewModel { get; private set; }

    [ObservableProperty]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
    public partial WaveChartViewModel WaveChartViewModel { get; private set; }

    [ObservableProperty]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
    public partial SpectrumChartViewModel SpectrumChartViewModel { get; private set; }

    [ObservableProperty]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
    public partial LevelsViewModel LevelsViewModel { get; private set; }

    private readonly ISettingsManager _settingsManager;
    private readonly IWaveGeneratorBuilder _waveGeneratorBuilder;
    private readonly ILogger<SignalViewModel> _logger;
    private readonly IMessenger _messenger;
    private readonly AudioEngine _engine;

    private AudioPlaybackDevice _device;
    private WaveGenerator _waveGenerator;

    public SignalViewModel(
        ISettingsManager settingsManager,
        IWaveGeneratorBuilder waveGeneratorBuilder,
        ILogger<SignalViewModel> logger,
        IMessenger messenger,
        AudioEngine engine,
        DateTimeSignalTypeViewModel dateTimeSignalTypeViewModel,
        WaveChartViewModel waveChartViewModel,
        SpectrumChartViewModel spectrumChartViewModel,
        LevelsViewModel levelsViewModel)
    {
        _settingsManager = settingsManager;
        _waveGeneratorBuilder = waveGeneratorBuilder;
        _logger = logger;
        _messenger = messenger;
        _engine = engine;
        DateTimeSignalTypeViewModel = dateTimeSignalTypeViewModel;
        WaveChartViewModel = waveChartViewModel;
        SpectrumChartViewModel = spectrumChartViewModel;
        LevelsViewModel = levelsViewModel;
    }

    [RelayCommand(CanExecute = nameof(CanPlay))]
    private Task Play()
    {
        IsInitializing = true;

        return Task.Run(() =>
        {
            _device = GetDevice();

            _waveGenerator = GetWaveGenerator();
            var waveSampler = GetWaveSampler();
            var waveLevelExtractor = GetWaveLevelExtractor();
            var spectrumBuilder = GetSpectrumBuilder();

            _device.MasterMixer.AddComponent(_waveGenerator);
            _device.MasterMixer.AddAnalyzer(waveSampler);
            _device.MasterMixer.AddAnalyzer(waveLevelExtractor);
            _device.MasterMixer.AddAnalyzer(spectrumBuilder);

            _device.Start();
        }).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                IsPlaying = false;

                var ex = task.Exception;
                const string message = "An error occurred while starting signal generation.";
                _logger?.LogError(ex, message);
                _messenger.Send(new ErrorMessage(message, ex));
            }
            else
            {
                IsPlaying = true;
            }

            IsInitializing = false;
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    private Task Stop()
    {
        IsFinalizing = true;

        return Task.Run(() =>
        {
            _device?.Stop();

            _waveGenerator?.Dispose();
            _waveGenerator = null;

            _device?.Dispose();
            _device = null;
        }).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                IsPlaying = true;

                var ex = task.Exception;
                const string message = "An error occurred while stopping signal generation.";
                _logger?.LogError(ex, message);
                _messenger.Send(new ErrorMessage(message, ex));
            }
            else
            {
                IsPlaying = false;

                WaveChartViewModel.ClearValues();
                SpectrumChartViewModel.ClearValues();
                LevelsViewModel.ClearValues();
            }

            IsFinalizing = false;
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public void Dispose()
    {
        _device?.Stop();
        _device?.Dispose();
        _device = null;

        _waveGenerator?.Dispose();
        _waveGenerator = null;

        GC.SuppressFinalize(this);
    }

    private bool CanPlay() => !IsPlaying && !IsInitializing;

    private bool CanStop() => IsPlaying && !IsFinalizing;

    private AudioPlaybackDevice GetDevice()
    {
        var format = _settingsManager.AudioFormat;

        _engine.UpdateAudioDevicesInfo();

        var defaultDevice = _engine.PlaybackDevices.FirstOrDefault(x => x.IsDefault);
        _device = _engine.InitializePlaybackDevice(defaultDevice, format);

        return _device;
    }

    private WaveGenerator GetWaveGenerator()
    {
        _waveGenerator = _waveGeneratorBuilder
            .InitFromSettings(_settingsManager)
            .Build();

        return _waveGenerator;
    }

    private WaveSampler GetWaveSampler()
    {
        var format = _settingsManager.AudioFormat;

        var waveSampler = new WaveSampler(
            format, sample => { WaveChartViewModel.PrependValue(sample); });

        return waveSampler;
    }

    private WaveLevelExtractor GetWaveLevelExtractor()
    {
        var format = _settingsManager.AudioFormat;

        var waveLevelExtractor = new WaveLevelExtractor(
            format,
            (peak, _) => { LevelsViewModel.Peak = MathF.Round(peak, 2); });

        return waveLevelExtractor;
    }

    private SpectrumBuilder GetSpectrumBuilder()
    {
        var format = _settingsManager.AudioFormat;

        var spectrumBuilder = new SpectrumBuilder(
            format,
            16 * 2,
            values =>
            {
                var vals = Array.ConvertAll(values, val => (double)val);
                SpectrumChartViewModel.SetValues(vals);
            });

        return spectrumBuilder;
    }
}