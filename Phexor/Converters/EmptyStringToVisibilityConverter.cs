using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Phexor.Converters;

public class EmptyStringToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return string.IsNullOrWhiteSpace(value as string) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Visibility and Visibility.Visible ? string.Empty : null;
    }
}
