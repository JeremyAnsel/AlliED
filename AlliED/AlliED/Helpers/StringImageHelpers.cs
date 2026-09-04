using AlliED.Extensions;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace AlliED.Helpers;

internal static class StringImageHelpers
{
    public static BitmapFrame? CreateFrame(string data)
    {
        byte[]? buffer = CreateFrameData(data);

        if (buffer is null)
        {
            return null;
        }

        BitmapFrame frame;
        using (var ms = new MemoryStream(buffer, 0, buffer.Length))
        {
            frame = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            frame.Freeze();
        }

        return frame;
    }

    public static byte[]? CreateFrameData(string data)
    {
        byte[]? buffer = CreateFrameDataRaw(data);

        if (buffer is null)
        {
            return null;
        }

        if (buffer.Length > 10)
        {
            if (BitConverter.ToUInt32(buffer, 6) == 0x4649464A)
            {
                // JFIF
                return buffer;
            }
        }

        int size = BitConverter.ToInt32(buffer, 2);

        if (buffer.Length < size)
        {
            int delta = size - buffer.Length;
            int width = BitConverter.ToInt32(buffer, 14 + 4);
            int height = BitConverter.ToInt32(buffer, 14 + 8);
            int bpp = BitConverter.ToInt16(buffer, 14 + 14) / 8;
            int length = width * height * bpp;

            byte[] buffer2 = new byte[size];

            if (bpp == 0)
            {
                buffer.CopyTo(buffer2, 0);
            }
            else
            {
                Array.Copy(buffer, 0, buffer2, 0, buffer.Length - length);
                Array.Copy(buffer, buffer.Length - length, buffer2, buffer2.Length - length, length);
            }

            buffer = buffer2;
        }

        return buffer;
    }

    public static byte[]? CreateFrameDataRaw(string data)
    {
        byte[] dataBuffer = StringHelpers.StringToByteArray(data);

        int position;

        if (dataBuffer[0] == 0x76 && dataBuffer[1] == 0x1A)
        {
            position = 8;
        }
        else if (dataBuffer[0] == 0x36 && dataBuffer[1] == 0x08)
        {
            position = 8;
        }
        else if (dataBuffer[0] == 0x49 && dataBuffer[1] == 0x4C)
        {
            position = 28;
        }
        else
        {
            string type;

            if (dataBuffer[3] == 0)
            {
                type = "Bitmap";
            }
            else
            {
                type = Encoding.ASCII.GetString(dataBuffer, 1, dataBuffer[0]);
            }

            if (string.Equals(type, "Bitmap", StringComparison.OrdinalIgnoreCase))
            {
                position = 4;
            }
            else if (string.Equals(type, "TIcon", StringComparison.OrdinalIgnoreCase))
            {
                position = 1 + dataBuffer[0];
            }
            else if (string.Equals(type, "TJPEGImage", StringComparison.OrdinalIgnoreCase))
            {
                position = 1 + dataBuffer[0] + 4;
            }
            else if (string.Equals(type, "TBitmap", StringComparison.OrdinalIgnoreCase))
            {
                position = 1 + dataBuffer[0] + 4;
            }
            else
            {
                return null;
            }
        }

        byte[] buffer = dataBuffer.Subarray(position, dataBuffer.Length - position);
        return buffer;
    }
}
