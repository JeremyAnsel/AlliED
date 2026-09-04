using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour ChoiceBox.xaml
/// </summary>
public partial class ChoiceBox : Window
{
    public ChoiceBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        this.VersionLab.Text = ProductVersionHelpers.GetVersion();
        this.Label2.Text = ProductVersionHelpers.GetCopyright();
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
