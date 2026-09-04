using AlliED.Helpers;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AlliED;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    static App()
    {
        ToolTipService.InitialShowDelayProperty.OverrideMetadata(
            typeof(FrameworkElement), new FrameworkPropertyMetadata(0));

        ToolTipService.BetweenShowDelayProperty.OverrideMetadata(
            typeof(FrameworkElement), new FrameworkPropertyMetadata(0));

        ToolTipService.InitialShowDelayProperty.OverrideMetadata(
            typeof(Hyperlink), new FrameworkPropertyMetadata(0));
    }

    protected override void OnStartup(StartupEventArgs e)
    {
#if !DEBUG
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

#if !DEBUG
        Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
#endif

        base.OnStartup(e);

        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
    }

#if !DEBUG
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var ex = e.ExceptionObject as Exception;
        Xceed.Wpf.Toolkit.MessageBox.Show(ex?.ToString(), "UnhandledException - Press Ctrl + C to copy the text");
    }
#endif

#if !DEBUG
    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;

        if (e.Exception is System.Runtime.InteropServices.COMException comException && comException.ErrorCode == -2147221040) // CLIPBRD_E_CANT_OPEN
        {
            return;
        }

        Xceed.Wpf.Toolkit.MessageBox.Show(e.Exception.ToString(), "Press Ctrl+C to copy the text", MessageBoxButton.OK, MessageBoxImage.Error);
        Application.Current.Shutdown();
    }
#endif
}
