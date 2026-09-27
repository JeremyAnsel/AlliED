using System.Windows.Controls.Primitives;

namespace AlliED.Extensions;

internal static class SelectorExtensions
{
    public static int SelectedIndexOr0(this Selector selector)
    {
        int index = selector.SelectedIndex;

        if (index == -1)
        {
            return 0;
        }

        return index;
    }
}
