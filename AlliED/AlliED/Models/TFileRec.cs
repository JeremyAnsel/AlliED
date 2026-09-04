using AlliED.Extensions;
using System.IO;

namespace AlliED.Models;

internal class TFileRec
{
    private string _filename = string.Empty;
    private FileStream? _file;
    private int _recSize;
    private int _fileLength;

    public void Assign(string filename)
    {
        if (_file is not null)
        {
            _file.Dispose();
            _file = null;
        }

        _filename = filename;
        _recSize = 0;
        _fileLength = 0;
    }

    public void Flush()
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Flush();
    }

    public bool EofFile()
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        return _file.Position == _fileLength;
    }

    public int FilePos()
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        return (int)_file.Position;
    }

    public int FileSize()
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        return _fileLength;
    }

    public void OpenFileForRead(int recSize)
    {
        if (_file is not null || string.IsNullOrEmpty(_filename))
        {
            throw new InvalidOperationException();
        }

        _file = File.OpenRead(_filename);
        _recSize = recSize;
        _fileLength = (int)_file.Length;
    }

    public void OpenFileForWrite(int recSize)
    {
        if (_file is not null || string.IsNullOrEmpty(_filename))
        {
            throw new InvalidOperationException();
        }

        _file = File.OpenWrite(_filename);
        _recSize = recSize;
        _fileLength = -1;
    }

    public void Close()
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Dispose();
        _file = null;
        _filename = string.Empty;
        _recSize = 0;
        _fileLength = 0;
    }

    public void BlockRead(byte[] array, int offset, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Read(array, offset, count);
    }

    public void BlockRead(byte[] array, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Read(array, 0, count);
    }

    public void BlockRead(byte[] array, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Read(array, 0, array.Length);
    }

    public void BlockRead(TFixedString array, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        byte[] buffer = new byte[count];
        _file.Read(buffer, 0, count);
        array.Text = buffer.ReadFixedLengthString(0, count);
    }

    public void BlockWrite(byte[] array, int offset, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Write(array, offset, count);
    }

    public void BlockWrite(byte[] array, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Write(array, 0, count);
    }

    public void BlockWrite(byte[] array, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        _file.Write(array, 0, array.Length);
    }

    public void BlockWrite(byte value, nint notUsed)
    {
        byte[] array = new byte[1];
        array[0] = value;
        BlockWrite(array, notUsed);
    }

    public void BlockWrite(TFixedString array, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        byte[] buffer = new byte[count];
        buffer.WriteFixedLengthString(0, array.Text, count);
        _file.Write(buffer, 0, count);
    }

    public void BlockWrite(string str, int count, nint notUsed)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        byte[] buffer = new byte[count];
        buffer.WriteFixedLengthString(0, str, count);
        _file.Write(buffer, 0, count);
    }

    public void ReadRec(byte[] array)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        if (array.Length != _recSize)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        _file.Read(array, 0, _recSize);
    }

    public void WriteRec(byte[] array)
    {
        if (_file is null)
        {
            throw new InvalidOperationException();
        }

        if (array.Length != _recSize)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        _file.Write(array, 0, _recSize);
    }
}
