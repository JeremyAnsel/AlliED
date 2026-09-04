using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AlliED.Converters;

internal sealed class RoutedCommandTextConverter : IValueConverter
{
    public static readonly RoutedCommandTextConverter Default = new();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not RoutedCommand command)
        {
            return null;
        }

        string text = command is RoutedUICommand uiCommand ? uiCommand.Text : command.Name;

        if (command.InputGestures is null || command.InputGestures.Count == 0)
        {
            return text;
        }

        string? keyText = null;

        foreach (var gesture in command.InputGestures)
        {
            if (gesture is KeyGesture keyGesture)
            {
                keyText = keyGesture.GetDisplayStringForCulture(culture);
                break;
            }
        }

        if (keyText is null)
        {
            return text;
        }

        var item = new StackPanel
        {
            Orientation = Orientation.Horizontal
        };

        item.Children.Add(new TextBlock
        {
            Text = text + " "
        });

        item.Children.Add(new TextBlock
        {
            Text = keyText,
            FontWeight = FontWeights.Light
        });

        return item;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
