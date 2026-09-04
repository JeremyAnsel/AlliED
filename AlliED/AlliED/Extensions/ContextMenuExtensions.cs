using System.Windows.Controls;

namespace AlliED.Extensions;

internal static class ContextMenuExtensions
{
    public static void SetM000061(this ContextMenu menu, bool value)
    {
        menu.IsEnabled = value;
    }
}
