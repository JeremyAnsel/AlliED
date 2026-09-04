using AlliED.Helpers;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

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

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(e.Uri.AbsoluteUri);
        e.Handled = true;
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
