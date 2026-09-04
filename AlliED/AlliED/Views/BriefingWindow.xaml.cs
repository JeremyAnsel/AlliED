using AlliED.Extensions;
using AlliED.Helpers;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour BriefingWindow.xaml
/// </summary>
public partial class BriefingWindow : Window
{
    public BriefingWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        BeginZoomBox.SetMaxDropDownCount(15);
        BeginOnFGBox.SetMaxDropDownCount(25);
        MapIndex.SetMaxDropDownCount(20);

        BrfOpenDialog1 = GetBrfOpenDialog();

        DataContext = this;
    }

    public ObservableCollection<CollectionTextItem> MapTagCollection { get; } =
        new ObservableCollection<CollectionTextItem>(
            Enumerable
            .Range(0, 32)
            .Select(t => new CollectionTextItem(string.Empty)));

    public ObservableCollection<CollectionTextItem> BrfStrCollection { get; } =
        new ObservableCollection<CollectionTextItem>(
            Enumerable
            .Range(0, 32)
            .Select(t => new CollectionTextItem(string.Empty)));

    public OpenFileDialog BrfOpenDialog1 { get; }

    public TimerEx Timer1 { get; } = new TimerEx
    {
        Interval = 25
    };

    public TimerEx Timer2 { get; } = new TimerEx
    {
        Interval = 40
    };

    private void Acc_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox)
        {
            return;
        }

        foreach (var button in new[] { Acc, Acc2, Dee, Dee2 })
        {
            if (sender != button)
            {
                button.IsChecked = false;
            }
        }
    }

    private OpenFileDialog GetBrfOpenDialog()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "XWA, XvT, T/F files (.tie)|*.tie|X-Wing Briefing files (.brf)|*.brf"
        };

        return dialog;
    }
}
