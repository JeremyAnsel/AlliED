using AlliED.Helpers;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour AboutBox.xaml
/// </summary>
public partial class AboutBox : Window
{
    public AboutBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        this.VersionLab.Text = ProductVersionHelpers.GetVersion();
        this.Label7.Text = ProductVersionHelpers.GetCopyright();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(e.Uri.AbsoluteUri);
        e.Handled = true;
    }
}
