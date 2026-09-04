using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour UnknownsWindow.xaml
/// </summary>
public partial class UnknownsWindow : Window
{
    public UnknownsWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
