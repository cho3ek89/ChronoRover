namespace ChronoRover.UI.Signal.ViewModels;

public class SpectrumChartViewModel : ValuesViewModel<double>
{
    public SpectrumChartViewModel()
    {
        Values = new double[16];
    }
}