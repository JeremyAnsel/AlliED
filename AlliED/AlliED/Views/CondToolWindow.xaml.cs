using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour CondToolWindow.xaml
/// </summary>
public partial class CondToolWindow : Window
{
    public CondToolWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }
}
