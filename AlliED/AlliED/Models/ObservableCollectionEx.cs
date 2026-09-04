using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AlliED.Models;

public class ObservableCollectionEx<T> : ObservableCollection<T> where T : INotifyPropertyChanged
{
    public ObservableCollectionEx() : base()
    {
        SelectedIndex = -1;
        CollectionChanged += new NotifyCollectionChangedEventHandler(ObservableCollectionEx_CollectionChanged);
    }

    public ObservableCollectionEx(IEnumerable<T> items)
        : this()
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public int SelectedIndex { get; set; }

    private void ObservableCollectionEx_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (T item in e.OldItems)
            {
                item.PropertyChanged -= EntityViewModelPropertyChanged;
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (T item in e.NewItems)
            {
                item.PropertyChanged += EntityViewModelPropertyChanged;
            }
        }
    }

    public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        SelectedIndex = IndexOf((T)sender);
        NotifyCollectionChangedEventArgs args = new(NotifyCollectionChangedAction.Reset);
        OnCollectionChanged(args);
    }
}
