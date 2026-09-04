using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour ImportBox.xaml
/// </summary>
public partial class ImportBox : Window
{
    public ImportBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
