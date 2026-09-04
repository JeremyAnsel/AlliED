using System.Windows;
using System.Windows.Controls;

namespace AlliED.Controls;

public static class MenuItemGroupBehavior
{
    public static readonly Dictionary<MenuItem, string> ElementToGroupNames = new();

    public static readonly DependencyProperty GroupNameProperty =
        DependencyProperty.RegisterAttached("GroupName", typeof(string), typeof(MenuItemGroupBehavior), new PropertyMetadata(string.Empty, OnGroupNameChanged));

    public static string GetGroupName(MenuItem element)
    {
        return element.GetValue(GroupNameProperty).ToString();
    }

    public static void SetGroupName(MenuItem element, string value)
    {
        element.SetValue(GroupNameProperty, value);
    }

    private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var menuItem = d as MenuItem;

        if (menuItem is null)
        {
            return;
        }

        string newGroupName = e.NewValue.ToString();
        string oldGroupName = e.OldValue.ToString();

        if (string.IsNullOrEmpty(newGroupName))
        {
            RemoveCheckboxFromGrouping(menuItem);
        }
        else
        {
            if (newGroupName != oldGroupName)
            {
                if (!string.IsNullOrEmpty(oldGroupName))
                {
                    RemoveCheckboxFromGrouping(menuItem);
                }
                ElementToGroupNames.Add(menuItem, e.NewValue.ToString());
                menuItem.Checked += MenuItemChecked;
                menuItem.Unchecked += MenuItemUnchecked;
            }
        }
    }

    private static void RemoveCheckboxFromGrouping(MenuItem checkBox)
    {
        ElementToGroupNames.Remove(checkBox);
        checkBox.Checked -= MenuItemChecked;
        checkBox.Unchecked -= MenuItemUnchecked;
    }


    private static void MenuItemChecked(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is not MenuItem menuItem)
        {
            return;
        }

        foreach (var item in ElementToGroupNames)
        {
            if (item.Key != menuItem && item.Value == GetGroupName(menuItem))
            {
                item.Key.IsChecked = false;
            }
        }
    }

    private static void MenuItemUnchecked(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is not MenuItem menuItem)
        {
            return;
        }

        var isAnyItemChecked = ElementToGroupNames.Any(item => item.Value == GetGroupName(menuItem) && item.Key.IsChecked);

        if (!isAnyItemChecked)
        {
            menuItem.IsChecked = true;
        }
    }
}
