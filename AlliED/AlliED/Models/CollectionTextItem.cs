using System.ComponentModel;

namespace AlliED.Models;

public sealed class CollectionTextItem : INotifyPropertyChanged
{
    public CollectionTextItem()
    {
    }

    public CollectionTextItem(string text)
    {
        Text = text;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

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
