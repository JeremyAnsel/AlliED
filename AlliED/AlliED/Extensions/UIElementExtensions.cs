using System.Windows;
using System.Windows.Controls;

namespace AlliED.Extensions;

internal static class UIElementExtensions
{
    public static void Update(this UIElement? element)
    {
        if (element is null)
        {
            return;
        }

        //element.UpdateLayout();
        //element.InvalidateVisual();
    }

    public static int GetLeft(this UIElement element)
    {
        return (int)Canvas.GetLeft(element);
    }

    public static int GetTop(this UIElement element)
    {
        return (int)Canvas.GetTop(element);
    }
}
