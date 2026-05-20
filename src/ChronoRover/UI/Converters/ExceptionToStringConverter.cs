using Avalonia.Data.Converters;

using System;
using System.Globalization;
using System.Text;

namespace ChronoRover.UI.Converters;

public class ExceptionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Exception exception)
            return null;

        var sb = new StringBuilder();

        sb.AppendLine("Message:");
        sb.AppendLine(exception.Message);

        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine("Stack trace:");
        sb.AppendLine(exception.StackTrace);

        return sb.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}