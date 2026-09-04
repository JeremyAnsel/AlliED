using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace AlliED.Controls;

public class ComboBoxEx : ComboBox
{
    public event RoutedEventHandler? ItemsChanged;

    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);
        ItemsChanged?.Invoke(this, new RoutedEventArgs());
    }

    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        ItemsChanged?.Invoke(this, new RoutedEventArgs());
    }
}
