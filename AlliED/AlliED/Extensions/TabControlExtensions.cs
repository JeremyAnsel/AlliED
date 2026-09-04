using System.Windows.Controls;

namespace AlliED.Extensions;

internal static class TabControlExtensions
{
    public static TabItem GetActivePage(this TabControl tabControl)
    {
        return (TabItem)tabControl.SelectedItem;
    }

    public static int GetPageIndex(this TabControl tabControl)
    {
        return tabControl.SelectedIndex;
    }
}
