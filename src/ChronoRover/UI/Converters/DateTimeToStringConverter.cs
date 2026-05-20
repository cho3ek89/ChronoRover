using Avalonia.Data.Converters;

using System;
using System.Globalization;

namespace ChronoRover.UI.Converters;

public class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var dateTime = (DateTime)value!;
        var format = parameter as string;

        return string.IsNullOrEmpty(format)
            ? dateTime.ToString(CultureInfo.InvariantCulture)
            : dateTime.ToString(format, CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}