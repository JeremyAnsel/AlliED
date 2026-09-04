using AlliED.Helpers;
using System.Windows;
using System.Windows.Input;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour MapWindow.xaml
/// </summary>
public partial class MapWindow : Window
{
    public MapWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
    }

    public event ExecutedRoutedEventHandler? Timer1Event;

    public Timer? Timer1 { get; private set; }

    public void EnableTimer1()
    {
        DisableTimer1();
        Timer1 = new Timer(Timer1Callback, null, 0, 30);
    }

    public void DisableTimer1()
    {
        Timer1?.Dispose();
        Timer1 = null;
    }

    private void Timer1Callback(object state)
    {
        Dispatcher.Invoke(() =>
        {
            Timer1Event?.Invoke(Timer1, null);
        });
    }
}
