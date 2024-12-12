using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Phexor.Converters;

public class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return new SolidColorBrush(color);
        }

        if (value is string colorString && ColorConverter.ConvertFromString(colorString) is Color convertedColor)
        {
            return new SolidColorBrush(convertedColor);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Color;
        }

        return null;
    }
}