using Avalonia.Controls;

using ChronoRover.UI.Signal.ViewModels;

using System;
using System.Threading;

namespace ChronoRover.UI.Signal.Views;

public partial class SpectrumChartView : UserControl, IDisposable
{
    private Timer _timer;

    public void Dispose()
    {
        _timer?.Dispose();

        GC.SuppressFinalize(this);
    }

    public SpectrumChartView()
    {
        InitializeComponent();

        SpecPlot.Plot.Axes.SetLimitsX(-0.5, 15.5);
        SpecPlot.Plot.Axes.SetLimitsY(0, 8.6);
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is ValuesViewModel<double> vmNew)
        {
            SpecPlot.Plot.Clear();

            var barColor = SpecPlot.LinesColor;

            var values = vmNew.GetValues();
            var bars = SpecPlot.Plot.Add.Bars(values).Bars;

            foreach (var bar in bars)
            {
                bar.LineColor = barColor;
                bar.FillColor = barColor;
            }

            SpecPlot.Refresh();

            _timer = new Timer(
                _ => UpdateBars(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(1000f / 40));

            void UpdateBars()
            {
                var newValues = vmNew.GetValues();
                var i = 0;
                foreach (var bar in bars)
                {
                    bar.Value = newValues[i++];
                }

                SpecPlot.Plot.Axes.AutoScaleExpandY();
                SpecPlot.Plot.Axes.SetLimits(bottom: 0);

                SpecPlot.Refresh();
            }
        }

        base.OnDataContextChanged(e);
    }
}