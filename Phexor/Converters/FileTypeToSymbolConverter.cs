using System;
using System.Globalization;
using System.Windows.Data;
using Phexor.Utilities;

namespace Phexor.Converters;

public class FileTypeToSymbolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string fileName)
        {
            return FileTypeHelper.GetSymbolForFileType(fileName);
        }

        return "\uE7C3"; // Default File Icon
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}