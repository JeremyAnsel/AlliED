using AlliED.Helpers;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour HeaderBox.xaml
/// </summary>
public partial class HeaderBox : Window
{
    public HeaderBox()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        DataContext = this;
    }

    public ObservableCollectionEx<CollectionTextItem> RegionNameCollection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 4)
            .Select(t => new CollectionTextItem(string.Empty)));

    public ObservableCollectionEx<CollectionTextItem> HeaderStringCollection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 4)
            .Select(t => new CollectionTextItem(string.Empty)));

    public ObservableCollectionEx<CollectionTextItem> Str3Collection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 72)
            .Select(t => new CollectionTextItem(string.Empty)));

    public ObservableCollectionEx<CollectionTextItem> Str2Collection { get; } =
        new ObservableCollectionEx<CollectionTextItem>(
            Enumerable
            .Range(0, 16)
            .Select(t => new CollectionTextItem(string.Empty)));

    private void CancelBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void OKBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}
