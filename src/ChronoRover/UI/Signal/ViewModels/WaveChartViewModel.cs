namespace ChronoRover.UI.Signal.ViewModels;

public class WaveChartViewModel : ValuesViewModel<double>
{
    public WaveChartViewModel()
    {
        Values = new double[300];
    }
}