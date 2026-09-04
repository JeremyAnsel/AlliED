using System.IO;
using System.Text;

namespace AlliED.Models;

internal class TStrings
{
    private static readonly Encoding _encoding = Encoding.GetEncoding("iso-8859-1");

    private readonly List<string> _list = new();

    public string[] Items => _list.ToArray();

    public TStrings()
    {
    }

    public TStrings(string[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            _list.Add(items[i]);
        }
    }

    public int GetCount()
    {
        return _list.Count;
    }

    public int IndexOf(string value)
    {
        return _list.IndexOf(value);
    }

    public string GetText(int index)
    {
        if (index == -1)
        {
            return string.Empty;
        }

        return _list[index];
    }

    public void Add(string value)
    {
        _list.Add(value);
    }

    public void Put(int index, string value)
    {
        _list[index] = value;
    }

    public void Insert(int index, string value)
    {
        _list.Insert(index, value);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public void Delete(int index)
    {
        _list.RemoveAt(index);
    }

    public void Exchange(int index1, int index2)
    {
        (_list[index1], _list[index2]) = (_list[index2], _list[index1]);
    }

    public void LoadFromFile(string filename)
    {
        string[] items = File.ReadAllLines(filename, _encoding);
        _list.Clear();
        _list.AddRange(items);
    }

    public void SaveToFile(string filename)
    {
        File.WriteAllLines(filename, _list, _encoding);
    }
}
