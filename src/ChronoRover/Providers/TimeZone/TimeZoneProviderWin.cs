namespace ChronoRover.Providers.TimeZone;

public class TimeZoneProviderWin : TimeZoneProviderBase
{
    protected override string GetDcf77TimeZoneId() => "Central European Standard Time";

    protected override string GetWwvbTimeZoneId() => "Mountain Standard Time";

    protected override string GetJjyTimeZoneId() => "Tokyo Standard Time";

    protected override string GetBpcTimeZoneId() => "China Standard Time";

    protected override string GetMsfTimeZoneId() => "GMT Standard Time";
}