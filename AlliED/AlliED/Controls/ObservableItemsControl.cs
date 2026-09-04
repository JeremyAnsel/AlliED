using System.Collections;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace AlliED.Controls;

public class ObservableItemsControl : ItemsControl
{
    public delegate void ObservableItemHandler(object? element, int index);

    public event ObservableItemHandler? ObservableItemChanged;

    private int _selectedIndex = -1;

    public int SelectedIndex => _selectedIndex;

    public int ColumnCount => Convert.ToInt32(Tag);

    private ObservableCollectionEx<CollectionTextItem>? _textCollection;
    private ObservableCollectionEx<CollectionSelectableTextItem>? _textCollectionSelectable;

    public ObservableItemsControl()
    {
        Unloaded += (s, e) => Reset();
    }

    public void CreateCollection(bool selectable)
    {
        if (selectable)
        {
            ItemsSource = new ObservableCollectionEx<CollectionSelectableTextItem>();
        }
        else
        {
            ItemsSource = new ObservableCollectionEx<CollectionTextItem>();
        }
    }

    public void AddItem(string text)
    {
        if (_textCollection is null)
        {
            throw new InvalidOperationException();
        }

        _textCollection.Add(new CollectionTextItem(text));
    }

    public void AddItem(string text, bool isSelected)
    {
        if (_textCollectionSelectable is null)
        {
            throw new InvalidOperationException();
        }

        _textCollectionSelectable.Add(new CollectionSelectableTextItem(text, isSelected));
    }

    private void Reset()
    {
        if (_textCollection is not null)
        {
            _textCollection.CollectionChanged -= TextCollection_CollectionChanged;
        }

        if (_textCollectionSelectable is not null)
        {
            _textCollectionSelectable.CollectionChanged -= TextCollectionSelectable_CollectionChanged;
        }

        _selectedIndex = -1;
        _textCollection = null;
        _textCollectionSelectable = null;
    }

    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);
        Reset();

        if (newValue is ObservableCollectionEx<CollectionTextItem> textCollection)
        {
            _textCollection = textCollection;
            textCollection.CollectionChanged += TextCollection_CollectionChanged;
        }
        else if (newValue is ObservableCollectionEx<CollectionSelectableTextItem> textCollectionSelectable)
        {
            _textCollectionSelectable = textCollectionSelectable;
            textCollectionSelectable.CollectionChanged += TextCollectionSelectable_CollectionChanged;
        }
    }

    private void TextCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        NotifyChanged(e, _textCollection!.SelectedIndex, _textCollection!.Count);
    }

    private void TextCollectionSelectable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        NotifyChanged(e, _textCollectionSelectable!.SelectedIndex, _textCollectionSelectable!.Count);
    }

    private void NotifyChanged(NotifyCollectionChangedEventArgs e, int index, int count)
    {
        if (e.Action != NotifyCollectionChangedAction.Reset)
        {
            return;
        }

        _selectedIndex = index;

        if (_selectedIndex >= count)
        {
            _selectedIndex = -1;
        }

        if (_selectedIndex == -1)
        {
            return;
        }

        object? element = null;

        if (_textCollection is not null)
        {
            element = _textCollection[_selectedIndex];
        }
        else if (_textCollectionSelectable is not null)
        {
            element = _textCollectionSelectable[_selectedIndex];
        }

        ObservableItemChanged?.Invoke(element, _selectedIndex);
    }
}
