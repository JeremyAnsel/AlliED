using System.ComponentModel;

namespace AlliED.Models;

public sealed class CollectionSelectableTextItem : INotifyPropertyChanged
{
    public CollectionSelectableTextItem()
    {
    }

    public CollectionSelectableTextItem(string text)
    {
        Text = text;
    }

    public CollectionSelectableTextItem(string text, bool isSelected)
    {
        Text = text;
        IsSelected = isSelected;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isSelected;

    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }

        set
        {
            if (value != _isSelected)
            {
                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }
    }

    private string _text = string.Empty;

    public string Text
    {
        get
        {
            return _text;
        }

        set
        {
            if (value != _text)
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }
    }

    public override string ToString()
    {
        return _text;
    }
}
