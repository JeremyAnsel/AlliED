using System.Windows;
using System.Windows.Input;

namespace AlliED.Models;

public class TimerEx
{
    public event ExecutedRoutedEventHandler? ElapsedEvent;

    private System.Timers.Timer? _timer;

    public int Interval { get; set; } = 15;

    private void Callback(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            ElapsedEvent?.Invoke(_timer, null);
        });
    }

    public void EnableTimer()
    {
        DisableTimer();
        _timer = new(Interval);
        _timer.Elapsed += Callback;
    }

    public void DisableTimer()
    {
        _timer?.Dispose();
        _timer = null;
    }

    public void Start()
    {
        EnableTimer();
        _timer!.Start();
    }

    public void Stop()
    {
        DisableTimer();
    }

    public bool GetM000048()
    {
        return _timer is not null;
    }

    public int GetM00000C()
    {
        return Interval;
    }

    public void SetM00000C(int value)
    {
        Interval = value;
    }
}
