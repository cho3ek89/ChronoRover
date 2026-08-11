using Avalonia.Controls;

using ChronoRover.UI.Signal.ViewModels;

using ScottPlot;

using System;
using System.Linq;
using System.Threading;

namespace ChronoRover.UI.Signal.Views;

public partial class WaveChartView : UserControl, IDisposable
{
    private Timer _timer;

    public void Dispose()
    {
        _timer?.Dispose();

        GC.SuppressFinalize(this);
    }

    public WaveChartView()
    {
        InitializeComponent();

        WavePlot.Plot.Axes.SetLimitsY(-1.1, -1.1);
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is ValuesViewModel<double> vmNew)
        {
            var newValues = vmNew.GetValues();
            var newArgs = Enumerable.Range(1, newValues.Length).ToArray();

            var signal = WavePlot.Plot.Add.SignalXY(newArgs, newValues);
            signal.Color = WavePlot.LinesColor;

            var xAxisRule = new WavePlotXAxisScalingRule(newValues.Length);
            WavePlot.Plot.Axes.Rules.Add(xAxisRule);

            WavePlot.Plot.Axes.SetLimitsY(-1.1, -1.1);
            WavePlot.Refresh();

            _timer = new Timer(
                _ => WavePlot.Refresh(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(1000f / 50));
        }

        base.OnDataContextChanged(e);
    }

    private class WavePlotXAxisScalingRule(float maxXLimit) : IAxisRule
    {
        private const float PixelsPerUnit = 3;

        public void Apply(RenderPack rp, bool beforeLayout)
        {
            if (beforeLayout)
                return;

            var rightEdge = rp.DataRect.Width / PixelsPerUnit;

            if (rightEdge > maxXLimit)
                rightEdge = maxXLimit;

            rp.Plot.Axes.SetLimitsX(0, rightEdge);
        }
    }
}