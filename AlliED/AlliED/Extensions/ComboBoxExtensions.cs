using System.Windows.Controls;
using System.Windows.Data;

namespace AlliED.Extensions;

internal static class ComboBoxExtensions
{
    public static void Clear(this ComboBox box)
    {
        if (BindingOperations.GetBinding(box, ItemsControl.ItemsSourceProperty) is not null)
        {
            BindingOperations.ClearBinding(box, ItemsControl.ItemsSourceProperty);
        }

        if (box.ItemsSource is not null)
        {
            box.ItemsSource = null;
            return;
        }

        box.Items.Clear();
    }

    public static void SetItems(this ComboBox box, ItemCollection items)
    {
        box.Clear();

        foreach (var item in items)
        {
            string content;

            if (item is ComboBoxItem boxItem)
            {
                content = (string)boxItem.Content;
            }
            else
            {
                content = (string)item;
            }

            box.AddItem(content);
        }
    }

    public static void SetItems(this ComboBox box, TStrings strings)
    {
        if (!AreItemsChanged(box, strings))
        {
            return;
        }

        if (box.ItemsSource is not null)
        {
            box.ItemsSource = strings.Items.Select(t => new ComboBoxItem { Content = t }).ToList();
            return;
        }

        box.Items.Clear();
        int count = strings.GetCount();
        for (int i = 0; i < count; i++)
        {
            string s = strings.GetText(i);
            var item = new ComboBoxItem { Content = s };
            box.Items.Add(item);
        }
    }

    private static bool AreItemsChanged(this ComboBox box, TStrings strings)
    {
        if (box.Items.Count == 0 && strings.GetCount() == 0)
        {
            return false;
        }

        if (box.Items.Count != strings.GetCount())
        {
            return true;
        }

        for (int index = 0; index < box.Items.Count; index++)
        {
            object item = box.Items[index];
            string left;
            if (item is string)
            {
                left = (string)item;
            }
            else
            {
                left = (string)((ComboBoxItem)item).Content;
            }

            string right = strings.GetText(index);

            if (!string.Equals(left, right, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    public static ComboBoxItem GetItem(this ComboBox box, int index)
    {
        object item = box.Items[index];

        if (item is ComboBoxItem boxItem)
        {
            return boxItem;
        }

        return new ComboBoxItem { Content = (string)item };
    }

    public static string GetItemText(this ComboBox box, int index)
    {
        return (string)box.GetItem(index).Content;
    }

    public static void AddItem(this ComboBox box, string str)
    {
        var item = new ComboBoxItem { Content = str };
        box.Items.Add(item);
    }

    public static void PutItem(this ComboBox box, int index, string str)
    {
        if (box.Items[index] is ComboBoxItem boxItem)
        {
            var item = (ComboBoxItem)box.Items[index];
            item.Content = str;
        }
        else
        {
            box.Items[index] = new ComboBoxItem { Content = str };
        }
    }

    public static void InsertItem(this ComboBox box, int index, string str)
    {
        var item = new ComboBoxItem { Content = str };
        box.Items.Insert(index, item);
    }

    public static void DeleteItem(this ComboBox box, int index)
    {
        box.Items.RemoveAt(index);
    }

    public static void LoadFromFile(this ComboBox box, string filename)
    {
        TStrings strings = new();
        strings.LoadFromFile(filename);
        box.SetItems(strings);
    }

    public static void SetMaxDropDownCount(this ComboBox box, int count)
    {
        if (box.Items.Count == 0)
        {
            return;
        }

        ComboBoxItem firstItem = box.GetItem(0);
        double height = firstItem.Height;
        box.MaxDropDownHeight = height * count;
    }
}
