using System.Collections.Specialized;
using System.Windows.Controls;

namespace AlliED.Controls;

public class CustomComboBox : ComboBox
{
    public delegate void DrawItemHandler(ComboBox sender, int index);

    public event DrawItemHandler? DrawItem;

    public CustomComboBox()
    {
    }

    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        CustomComboBox_UpdateItems();
    }

    private void CustomComboBox_UpdateItems()
    {
        for (int index = 0; index < Items.Count; index++)
        {
            DrawItem?.Invoke(this, index);
        }
    }
}
