using Avalonia.Data.Converters;

using System;
using System.Globalization;

namespace ChronoRover.UI.Converters;

public class EnumToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = Enum.GetName(value!.GetType(), value);

        return str!.ToUpperInvariant();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}