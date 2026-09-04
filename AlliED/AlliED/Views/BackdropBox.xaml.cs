using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour BackdropBox.xaml
/// </summary>
public partial class BackdropBox : Window
{
    public BackdropBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
