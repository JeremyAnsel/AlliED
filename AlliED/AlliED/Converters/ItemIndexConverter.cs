using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AlliED.Converters;

public sealed class ItemIndexConverter : FrameworkContentElement, IValueConverter
{
    public ItemIndexConverter()
    {
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (DataContext is not IList list)
        {
            return string.Empty;
        }

        int start = 0;

        if (parameter is string parameterString)
        {
            start = int.Parse(parameterString);
        }

        int index = start + list.IndexOf(value);

        return index.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
