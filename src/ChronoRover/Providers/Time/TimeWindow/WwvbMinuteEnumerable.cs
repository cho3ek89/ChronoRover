namespace ChronoRover.Providers.Time.TimeWindow;

public class WwvbMinuteEnumerable(ITimeProvider timeProvider) : MinuteEnumerable
{
    protected override void Initialize()
    {
        Minute = timeProvider.GetUtcTime();
    }
}