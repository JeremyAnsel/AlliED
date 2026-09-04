using AlliED.Helpers;
using System.Globalization;
using System.Windows.Data;

namespace AlliED.Converters;

internal sealed class StringImageConverter : IValueConverter
{
    public static readonly StringImageConverter Default = new();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string data)
        {
            return null;
        }

        return StringImageHelpers.CreateFrame(data);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
