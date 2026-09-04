using AlliED.Helpers;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace AlliED.Views;

/// <summary>
/// Logique d'interaction pour OrderSelWindow.xaml
/// </summary>
public partial class OrderSelWindow : Window
{
    public OrderSelWindow()
    {
        InitializeComponent();
        WindowHelpers.ShowAccessKeys(this);

        DataContext = this;
    }

    public ObservableCollection<CollectionTextItem> OrderSelCollection { get; } =
        new ObservableCollection<CollectionTextItem>(
            Enumerable
            .Range(1, 4)
            .Select(t => new CollectionTextItem(t.ToString(CultureInfo.InvariantCulture))));
}
