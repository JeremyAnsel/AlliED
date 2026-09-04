using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour TimeForm.xaml
/// </summary>
public partial class TimeForm : Window
{
    private Timer? Timer1;

    public static void ShowTimeForm(Window owner, string message = "message", int interval = 2000)
    {
        var time = new TimeForm(owner, message, interval);
        time.ShowDialog();
    }

    private TimeForm(Window owner, string message, int interval)
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);
        this.Owner = owner;

        Label1.Text = message;
        Timer1 = new Timer(new TimerCallback(Timer1Timer));
        Timer1.Change(interval, 0);

        Closed += TimeForm_Closed;
    }

    private void TimeForm_Closed(object sender, EventArgs e)
    {
        Timer1?.Dispose();
        Timer1 = null;
    }

    private void Timer1Timer(object state)
    {
        Timer t = (Timer)state;
        t.Dispose();
        Timer1 = null;
        Dispatcher.Invoke(() => Close());
    }
}
