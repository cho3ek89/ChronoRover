namespace ChronoRover.Providers.TimeZone;

public class TimeZoneProvider : TimeZoneProviderBase
{
    protected override string GetDcf77TimeZoneId() => "Europe/Berlin";

    protected override string GetWwvbTimeZoneId() => "America/Denver";

    protected override string GetJjyTimeZoneId() => "Asia/Tokyo";

    protected override string GetBpcTimeZoneId() => "Asia/Shanghai";

    protected override string GetMsfTimeZoneId() => "Europe/London";
}