using System.Windows;
using System.Windows.Interop;

namespace AlliED.Helpers;

internal static class WindowHelpers
{
    public static void ShowAccessKeys(Window window)
    {
        if (GlobalSettings.AlwaysShowAccessKeys)
        {
            window.LayoutUpdated += (s, e) =>
            {
                nint handle = new WindowInteropHelper(window).Handle;
                NativeMethods.SendMessage(handle, NativeMethods.WM_UPDATEUISTATE, 0x20002, 0);
            };
        }
    }
}
