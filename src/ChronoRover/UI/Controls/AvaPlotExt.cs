using Avalonia;
using Avalonia.Media;

using ScottPlot.Avalonia;
using ScottPlot.Plottables;

using System;

namespace ChronoRover.UI.Controls;

public class AvaPlotExt : AvaPlot
{
    public static readonly StyledProperty<IBrush> LinesBrushProperty =
        AvaloniaProperty.Register<AvaPlotExt, IBrush>(
            nameof(LinesBrush),
            defaultValue: null);

    public IBrush LinesBrush
    {
        get => GetValue(LinesBrushProperty);
        set => SetValue(LinesBrushProperty, value);
    }

    public ScottPlot.Color LinesColor
    {
        get
        {
            if (LinesBrush == null)
                return default;

            if (LinesBrush is not ISolidColorBrush brush)
                throw new NotSupportedException("Provided brush type is not supported.");

            var color = brush.Color;
            return new ScottPlot.Color(color.R, color.G, color.B, color.A);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property != LinesBrushProperty)
            return;

        foreach (var plottable in Plot.GetPlottables())
        {
            var linesColor = LinesColor;

            switch (plottable)
            {
                case SignalXY signalXy:
                    signalXy.Color = linesColor;
                    break;
                case BarPlot plot:
                    plot.Color = linesColor;
                    break;
            }
        }
    }

    public AvaPlotExt()
    {
        // Make it read-only
        UserInputProcessor.IsEnabled = false;
        UserInputProcessor.Reset();

        // Hide axis labels and ticks
        Plot.Axes.Left.IsVisible = false;
        Plot.Axes.Right.IsVisible = false;
        Plot.Axes.Top.IsVisible = false;
        Plot.Axes.Bottom.IsVisible = false;

        // Set axis color
        // Plot.Axes.Color(ScottPlot.Colors.Black);

        // Style grid lines
        // Plot.Grid.LinePattern = ScottPlot.LinePattern.Dashed;
        // Plot.Grid.LineColor = ScottPlot.Colors.LightGray;

        // Hide grid lines
        Plot.Grid.IsVisible = false;

        // Make background transparent
        var style = Plot.GetStyle();
        style.FigureBackgroundColor = ScottPlot.Colors.Transparent;
        style.DataBackgroundColor = ScottPlot.Colors.Transparent;
        Plot.SetStyle(style);
    }
}