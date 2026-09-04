using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour DatapadWindow.xaml
/// </summary>
public partial class DatapadWindow : Window
{
    public DatapadWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }
}
