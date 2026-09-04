using System.Windows.Controls;
using System.Windows.Data;

namespace AlliED.Extensions;

internal static class ListBoxExtensions
{
    public static void Clear(this ListBox box)
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

        if (box.Items.Count == 0)
        {
            return;
        }

        box.Items.Clear();
    }

    public static void SetItems(this ListBox box, TStrings strings)
    {
        if (box.ItemsSource is not null)
        {
            box.ItemsSource = strings.Items.Select(t => new ListBoxItem { Content = t }).ToList();
            return;
        }

        box.Items.Clear();
        int count = strings.GetCount();
        for (int i = 0; i < count; i++)
        {
            string s = strings.GetText(i);
            var item = new ListBoxItem { Content = s };
            box.Items.Add(item);
        }
    }

    public static ListBoxItem GetItem(this ListBox box, int index)
    {
        return (ListBoxItem)box.Items[index];
    }

    public static string GetItemText(this ListBox box, int index)
    {
        return (string)box.GetItem(index).Content;
    }

    public static void AddItem(this ListBox box, string str)
    {
        var item = new ListBoxItem { Content = str };
        box.Items.Add(item);
    }

    public static void InsertItem(this ListBox box, int index, string str)
    {
        var item = new ListBoxItem { Content = str };
        box.Items.Insert(index, item);
    }

    public static void PutItem(this ListBox box, int index, string str)
    {
        var item = (ListBoxItem)box.Items[index];
        item.Content = str;
    }

    public static void DeleteItem(this ListBox box, int index)
    {
        box.Items.RemoveAt(index);
    }

    public static void LoadFromFile(this ListBox box, string filename)
    {
        TStrings strings = new();
        strings.LoadFromFile(filename);
        box.SetItems(strings);
    }

    public static void SaveToFile(this ListBox box, string filename)
    {
        TStrings strings = new();
        for (int i = 0; i < box.Items.Count; i++)
        {
            strings.Add(box.GetItemText(i));
        }
        strings.SaveToFile(filename);
    }

    public static void Exchange(this ListBox box, int index1, int index2)
    {
        (box.Items[index1], box.Items[index2]) = (box.Items[index2], box.Items[index1]);
    }

    public static void M0000F0(this ListBox box, bool value)
    {
        // todo
    }
}
