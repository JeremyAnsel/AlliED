using System.Windows.Controls;
using System.Windows.Media;

namespace AlliED.Extensions;

internal static class ControlExtensions
{
    public static Color GetFontColor(this Control control)
    {
        if (control.Foreground is not SolidColorBrush brush)
        {
            throw new InvalidOperationException();
        }

        return brush.Color;
    }

    public static int GetClientWidth(this Control control)
    {
        return (int)control.ActualWidth;
    }

    public static int GetClientHeight(this Control control)
    {
        return (int)control.ActualHeight;
    }

    public static int GetM000220(this Control control)
    {
        // todo
        //return control.IsEnabled ? 1 : 0;

        return 0;
    }

    public static void SetM000220(this Control control, int value)
    {
        // todo
        //control.IsEnabled = value != 0;
    }
}
