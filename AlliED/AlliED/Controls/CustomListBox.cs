using System.Collections.Specialized;
using System.Windows.Controls;

namespace AlliED.Controls;

public class CustomListBox : ListBox
{
    public delegate void DrawItemHandler(ListBox sender, int index);

    public event DrawItemHandler? DrawItem;

    public CustomListBox()
    {
    }

    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        CustomListBox_UpdateItems();
    }

    private void CustomListBox_UpdateItems()
    {
        for (int index = 0; index < Items.Count; index++)
        {
            DrawItem?.Invoke(this, index);
        }
    }
}
