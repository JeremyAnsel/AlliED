using SharpDialogs.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AlliED;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly bool _autoLaunchLastForm = System.Diagnostics.Debugger.IsAttached;
    private readonly bool _autoLaunchLastForm = false;

    //private readonly bool _autoCloseWhenFormIsClosed = System.Diagnostics.Debugger.IsAttached;
    private readonly bool _autoCloseWhenFormIsClosed = false;

    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (_autoLaunchLastForm)
        {
            Button button = buttonsPanel
                .Children
                .OfType<Button>()
                .LastOrDefault();

            button?.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
    }

    private void FolderBrowserTestButton_Click(object sender, RoutedEventArgs e)
    {
        string? result = SharpFolderBrowserDialogWpf.ShowSingleSelect(this, "Select a directory");

        if (result is not null)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show($"The selected folder is \"{result}\".", "Select a directory", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void TimeFormTestButton_Click(object sender, RoutedEventArgs e)
    {
        TimeForm.ShowTimeForm(this);
    }

    private void ShowForm_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        if (button.Content is not string typeName)
        {
            return;
        }

        Type? type = Type.GetType("AlliED.Views." + typeName, false);

        if (type is null)
        {
            return;
        }

        if (Activator.CreateInstance(type) is not Window window)
        {
            return;
        }

        window.Owner = this;
        window.WindowStyle = WindowStyle.ToolWindow;
        window.ShowInTaskbar = false;
        window.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

        window.ShowDialog();

        if (_autoCloseWhenFormIsClosed)
        {
            Close();
        }
    }

    private void Main_Click(object sender, RoutedEventArgs e)
    {
        //MainImpl.Run();
    }
}
