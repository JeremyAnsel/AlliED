using System.Windows.Controls;

namespace AlliED.Extensions;

internal static class TextBoxExtensions
{
    public static void SetItems(this TextBox textBox, TStrings items)
    {
        textBox.Text = string.Join("\n", items.Items);
    }

    public static void AddLine(this TextBox textBox, string line)
    {
        if (!string.IsNullOrEmpty(textBox.Text))
        {
            textBox.Text += "\n";
        }

        textBox.Text += line;
    }

    public static bool M000057(this TextBox? textBox)
    {
        // todo

        if (textBox is null)
        {
            return false;
        }

        return textBox.IsVisible;
    }
}
