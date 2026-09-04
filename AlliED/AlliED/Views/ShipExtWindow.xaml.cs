using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour ShipExtWindow.xaml
/// </summary>
public partial class ShipExtWindow : Window
{
    public ShipExtWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }
}
