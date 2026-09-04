using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour LstWindow.xaml
/// </summary>
public partial class LstWindow : Window
{
    public LstWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void BitBtn1_Click(object sender, RoutedEventArgs e)
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
