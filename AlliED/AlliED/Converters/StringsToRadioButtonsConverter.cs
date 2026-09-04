using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace AlliED.Converters;

internal sealed class StringsToRadioButtonsConverter : IMultiValueConverter
{
    public static readonly StringsToRadioButtonsConverter Default = new();

    public object? Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2)
        {
            return null;
        }

        if (values[0] is not Panel panel)
        {
            return null;
        }

        if (values[1] is not string[] data)
        {
            return null;
        }

        panel.Children.Clear();

        foreach (string d in data)
        {
            var item = new RadioButton
            {
                Content = d
            };

            panel.Children.Add(item);
        }

        if (panel.Children.Count > 0)
        {
            ((RadioButton)panel.Children[0]).IsChecked = true;
        }

        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
