using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour WavListBox.xaml
/// </summary>
public partial class WavListBox : Window
{
    public WavListBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void OKBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
