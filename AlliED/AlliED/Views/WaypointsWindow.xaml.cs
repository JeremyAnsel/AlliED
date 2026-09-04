using AlliED.Helpers;
using System.Collections.ObjectModel;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour WaypointsWindow.xaml
/// </summary>
public partial class WaypointsWindow : Window
{
    public ObservableCollection<CollectionTextItem> WPRawCollection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 3 * 12)
            .Select(t => new CollectionTextItem("0.00")));

    public int WPRawCollectionColumnCount { get; } = 3;

    public ObservableCollection<CollectionTextItem> WPCollection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 3 * 6)
            .Select(t => new CollectionTextItem("0.00")));

    public int WPCollectionColumnCount { get; } = 3;

    public ObservableCollection<CollectionSelectableTextItem> WPEnabledCollection { get; } =
        new ObservableCollectionEx<CollectionSelectableTextItem>(
            WaypointsWindowResources.WPEnabledListStrings
            .Select(t => new CollectionSelectableTextItem(t, false)));

    public int WPEnabledCollectionColumnCount { get; } = 1;

    public ObservableCollection<CollectionTextItem> OrderWPCollection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 3 * 8)
            .Select(t => new CollectionTextItem("0.00")));

    public int OrderWPCollectionColumnCount { get; } = 3;

    public ObservableCollection<CollectionSelectableTextItem> OrderWPEnabledCollection { get; } =
        new ObservableCollectionEx<CollectionSelectableTextItem>(
            WaypointsWindowResources.OrderWPEnabledListStrings
            .Select(t => new CollectionSelectableTextItem(t, false)));

    public int OrderWPEnabledCollectionColumnCount { get; } = 1;

    public WaypointsWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        this.DataContext = this;
    }
}
