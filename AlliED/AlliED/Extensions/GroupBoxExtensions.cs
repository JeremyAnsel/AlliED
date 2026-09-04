using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlliED.Extensions;

internal static class GroupBoxExtensions
{
    public static void SetItemIndex(this GroupBox group, int index)
    {
        if (group.Content is not Panel panel)
        {
            throw new InvalidOperationException();
        }

        RadioButton button = (RadioButton)panel.Children[index];
        button.IsChecked = true;
    }

    public static int GetItemIndex(this GroupBox group)
    {
        if (group.Content is not Panel panel)
        {
            throw new InvalidOperationException();
        }

        for (int index = 0; index < panel.Children.Count; index++)
        {
            RadioButton button = (RadioButton)panel.Children[index];

            if (button.IsChecked == true)
            {
                return index;
            }
        }

        return -1;
    }

    public static bool M000057(this GroupBox? group)
    {
        // todo

        if (group is null)
        {
            return false;
        }

        return group.IsVisible;
    }

    public static void SetMouseLeftButtonUp(this GroupBox group, RoutedEventHandler handler)
    {
        if (group.Content is not Panel panel)
        {
            throw new InvalidOperationException();
        }

        for (int index = 0; index < panel.Children.Count; index++)
        {
            RadioButton button = (RadioButton)panel.Children[index];
            button.Click += handler;
        }
    }
}
