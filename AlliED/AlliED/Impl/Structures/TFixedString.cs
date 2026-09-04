using AlliED.Extensions;

namespace AlliED.Impl.Structures;

/// <remarks>
/// T0xTieHeader_00000E
/// T0x005346AC_000000
/// </remarks>
internal class TFixedString
{
    public TFixedString(int maxLength)
    {
        MaxLength = maxLength;
    }

    public TFixedString(int maxLength, string text)
    {
        MaxLength = maxLength;
        Text = text;
    }

    public int MaxLength { get; }

    private string _text = string.Empty;

    public string Text
    {
        get => _text;
        set => _text = value.WithMaxLength(MaxLength);
    }

    public char[] ToCharArray()
    {
        char[] array = new char[MaxLength];
        _text.ToCharArray().CopyTo(array, 0);
        return array;
    }

    public void UpdateCharArray(char[] array)
    {
        string str = new string(array);
        int index = str.IndexOf('\0');
        if (index != -1)
        {
            str = str[..index];
        }
        Text = str;
    }
}
