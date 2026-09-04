using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour XvTBox.xaml
/// </summary>
public partial class XvTBox : Window
{
    public XvTBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    private void OkBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
}
