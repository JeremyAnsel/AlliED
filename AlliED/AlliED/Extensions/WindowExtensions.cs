using System.Windows;

namespace AlliED.Extensions;

internal static class WindowExtensions
{
    public static TRect GetClientRect(this Window window)
    {
        // todo

        //return new TRect(
        //    (int)window.Left,
        //    (int)window.Top,
        //    (int)window.Left + (int)window.Width,
        //    (int)window.Top + (int)window.Height);

        return new TRect(
            0,
            0,
            (int)window.ActualWidth,
            (int)window.ActualHeight);
    }

    public static bool M000057(this Window? window)
    {
        // todo

        if (window is null)
        {
            return false;
        }

        return window.IsVisible;
    }

    public static FrameworkElement M000074(this Window window)
    {
        return (FrameworkElement)window.Content;
    }

    public static int M00022B(this Window window)
    {
        return window.WindowState == WindowState.Maximized ? 0x02 : 0;
    }
}
