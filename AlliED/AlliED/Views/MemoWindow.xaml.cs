using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour MemoWindow.xaml
/// </summary>
public partial class MemoWindow : Window
{
    public MemoWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }
}
