using AlliED.Helpers;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AlliED.Converters;

internal sealed class StringImageListSelectConverter : IValueConverter
{
    public static readonly StringImageListSelectConverter Default = new();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string data)
        {
            return null;
        }

        int splitWidth = 0;
        int splitHeight = 0;
        int frameIndex = 0;

        if (parameter is string parameterSize)
        {
            string[] parts = parameterSize.Split('-');

            if (parts.Length == 1)
            {
                frameIndex = int.Parse(parts[0], CultureInfo.InvariantCulture);
            }
            else
            {
                splitWidth = int.Parse(parts[0], CultureInfo.InvariantCulture);
                splitHeight = int.Parse(parts[1], CultureInfo.InvariantCulture);

                if (parts.Length >= 3)
                {
                    frameIndex = int.Parse(parts[2], CultureInfo.InvariantCulture);
                }
            }
        }

        byte[] dataBuffer = StringHelpers.StringToByteArray(data);

        int position;

        if (dataBuffer[0] == 0x76 && dataBuffer[1] == 0x1A)
        {
            position = 8;
        }
        else if (dataBuffer[0] == 0x36 && dataBuffer[1] == 0x08)
        {
            position = 8;

            if (splitWidth == 0 || splitHeight == 0)
            {
                splitWidth = 16;
                splitHeight = 16;
            }
        }
        else if (dataBuffer[0] == 0x49 && dataBuffer[1] == 0x4C)
        {
            position = 28;

            if (splitWidth == 0 || splitHeight == 0)
            {
                splitWidth = BitConverter.ToUInt16(dataBuffer, 10);
                splitHeight = BitConverter.ToUInt16(dataBuffer, 12);
            }
        }
        else
        {
            return null;
        }

        // "BM"
        if (position >= dataBuffer.Length || dataBuffer[position] != 0x42 || dataBuffer[position + 1] != 0x4D)
        {
            return null;
        }

        // header length
        if (BitConverter.ToInt32(dataBuffer, position + 10) != 54)
        {
            return null;
        }

        int position1 = position;
        int w1 = BitConverter.ToInt32(dataBuffer, position + 18);
        int h1 = BitConverter.ToInt32(dataBuffer, position + 22);
        int b1 = dataBuffer[position + 28];
        int length1 = 54 + w1 * h1 * b1 / 8;
        position += length1;

        // "BM"
        if (position >= dataBuffer.Length || dataBuffer[position] != 0x42 || dataBuffer[position + 1] != 0x4D)
        {
            return null;
        }

        // header length
        if (BitConverter.ToInt32(dataBuffer, position + 10) != 62)
        {
            return null;
        }

        int position2 = position;
        int length2 = 62 + BitConverter.ToInt32(dataBuffer, position + 34);
        position += length2;

        if (position > dataBuffer.Length)
        {
            return null;
        }

        BitmapFrame frame;

        using (var msColor = new MemoryStream(dataBuffer, position1, length1))
        using (var msTransparent = new MemoryStream(dataBuffer, position2, length2))
        {
            frame = BitmapHelpers.CreateBitmapFrame(msColor, msTransparent);
            frame.Freeze();
        }

        if (parameter is null || splitWidth == 0 || splitHeight == 0)
        {
            return frame;
        }

        int currentFrameIndex = -1;
        int frameWidth = frame.PixelWidth;
        int frameHeight = frame.PixelHeight;
        int bpp = frame.Format.BitsPerPixel / 8;
        int stride = splitWidth * Math.Max(bpp, 1);

        for (int y = 0; y < frameHeight; y += splitHeight)
        {
            for (int x = 0; x < frameWidth; x += splitWidth)
            {
                currentFrameIndex++;

                if (currentFrameIndex == frameIndex)
                {
                    var buffer = new byte[splitHeight * stride];
                    frame.CopyPixels(new System.Windows.Int32Rect(x, y, splitWidth, splitHeight), buffer, stride, 0);

                    BitmapSource subSource = BitmapSource.Create(splitWidth, splitHeight, 96.0, 96.0, frame.Format, frame.Palette, buffer, stride);

                    BitmapFrame subFrame = BitmapFrame.Create(subSource);
                    subFrame.Freeze();

                    return subFrame;
                }
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
