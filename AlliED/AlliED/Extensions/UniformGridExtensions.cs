using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AlliED.Extensions;

internal static class UniformGridExtensions
{
    public static void SetItemIndex(this UniformGrid group, int index)
    {
        RadioButton button = (RadioButton)group.Children[index];
        button.IsChecked = true;
    }

    public static int GetItemIndex(this UniformGrid group)
    {
        for (int index = 0; index < group.Children.Count; index++)
        {
            RadioButton button = (RadioButton)group.Children[index];

            if (button.IsChecked == true)
            {
                return index;
            }
        }

        return -1;
    }

    public static void SetClickHandler(this UniformGrid group, Action<object, RoutedEventArgs> handler)
    {
        for (int index = 0; index < group.Children.Count; index++)
        {
            RadioButton button = (RadioButton)group.Children[index];
            button.Click += (s, e) => handler(s, e);
        }
    }
}
