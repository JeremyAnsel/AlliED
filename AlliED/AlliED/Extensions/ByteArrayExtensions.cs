using System.Text;

namespace AlliED.Extensions;

internal static class ByteArrayExtensions
{
    public static byte[] Subarray(this byte[] array, int startIndex, int length)
    {
        byte[] sub = new byte[length];

        Array.Copy(array, startIndex, sub, 0, length);

        return sub;
    }

    public static byte[] Subarray(this byte[] array, int startIndex, int index, int length)
    {
        return array.Subarray(startIndex + index * length, length);
    }

    public static string ReadFixedLengthString(this byte[] array, int startIndex, int length)
    {
        string text = Encoding.ASCII.GetString(array, startIndex, length);

        int index = text.IndexOf('\0');

        if (index == -1)
        {
            return text;
        }

        return text[..index];
    }

    public static string ReadFixedLengthString(this byte[] array, int startIndex, int index, int length)
    {
        return array.ReadFixedLengthString(startIndex + index * length, length);
    }

    public static void WriteFixedLengthString(this byte[] array, int destinationIndex, string text, int length)
    {
        if (text == null)
        {
            Array.Clear(array, destinationIndex, length);
            return;
        }

        var bytes = Encoding.ASCII.GetBytes(text);
        int count = Math.Min(bytes.Length, length - 1);

        Array.Copy(bytes, 0, array, destinationIndex, count);
        Array.Clear(array, destinationIndex + count, length - count);
    }

    public static void WriteFixedLengthString(this byte[] array, int destinationIndex, int index, string text, int length)
    {
        array.WriteFixedLengthString(destinationIndex + index * length, text, length);
    }

    public static void ReadUnknown(this byte[] array, int startIndex, byte[] unk)
    {
        Array.Copy(array, startIndex, unk, 0, unk.Length);
    }

    public static void WriteUnknown(this byte[] array, int destinationIndex, byte[] unk)
    {
        unk.CopyTo(array, destinationIndex);
    }
}
