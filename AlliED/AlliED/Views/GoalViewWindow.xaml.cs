using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour GoalViewWindow.xaml
/// </summary>
public partial class GoalViewWindow : Window
{
    public GoalViewWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }
}
