using System.IO;
using System.Windows.Controls;

namespace AlliED.Models;

internal static class TStringsExtensions
{
    // L0051AEC0
    public static void AlliedCopyComboxBoxItemsToTStrings(this ComboBox box, TStrings strings)
    {
        strings.Clear();

        int count = box.Items.Count;
        for (int index = 0; index < count; index++)
        {
            object item = box.Items[index];

            if (item is string str)
            {
                strings.Add(str);
            }
            else if (item is ComboBoxItem boxItem)
            {
                string boxItemStr = (string)boxItem.Content;
                strings.Add(boxItemStr);
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}
