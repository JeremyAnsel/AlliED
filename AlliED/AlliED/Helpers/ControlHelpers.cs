using System.Windows.Controls;

namespace AlliED.Helpers;

internal static class ControlHelpers
{
    public static void SetParent(ContentControl source, ContentControl destination)
    {
        var content = source.Content;
        source.Content = null;
        destination.Content = content;
    }
}
