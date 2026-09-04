using AlliED.Helpers;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AlliED.Converters;

internal sealed class StringImageListConverter : IValueConverter
{
    public static readonly StringImageListConverter Default = new();

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string data)
        {
            return null;
        }

        int splitWidth = 0;
        int splitHeight = 0;
        int framesCount = 0;

        if (parameter is string parameterSize)
        {
            string[] parts = parameterSize.Split('-');

            splitWidth = int.Parse(parts[0], CultureInfo.InvariantCulture);
            splitHeight = int.Parse(parts[1], CultureInfo.InvariantCulture);

            if (parts.Length >= 3)
            {
                framesCount = int.Parse(parts[2], CultureInfo.InvariantCulture);
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
        }
        else
        {
            return null;
        }

        var frames = new List<BitmapFrame>();

        // "BM"
        while (position < dataBuffer.Length && dataBuffer[position] == 0x42 && dataBuffer[position + 1] == 0x4D)
        {
            if (framesCount != 0 && frames.Count >= framesCount)
            {
                break;
            }

            int length = BitConverter.ToInt32(dataBuffer, position + 2);
            BitmapFrame frame;

            using (var ms = new MemoryStream(dataBuffer, position, length))
            {
                frame = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                frame.Freeze();
            }

            position += length;

            if (splitWidth == 0 || splitHeight == 0)
            {
                frames.Add(frame);
            }
            else
            {
                int frameWidth = frame.PixelWidth;
                int frameHeight = frame.PixelHeight;
                int bpp = frame.Format.BitsPerPixel / 8;
                int stride = splitWidth * Math.Max(bpp, 1);

                var buffer = new byte[splitHeight * stride];

                for (int y = 0; y < frameHeight; y += splitHeight)
                {
                    for (int x = 0; x < frameWidth; x += splitWidth)
                    {
                        if (framesCount != 0 && frames.Count >= framesCount)
                        {
                            return frames;
                        }

                        frame.CopyPixels(new System.Windows.Int32Rect(x, y, splitWidth, splitHeight), buffer, stride, 0);

                        BitmapSource subSource = BitmapSource.Create(splitWidth, splitHeight, 96.0, 96.0, frame.Format, frame.Palette, buffer, stride);

                        BitmapFrame subFrame = BitmapFrame.Create(subSource);
                        subFrame.Freeze();

                        frames.Add(subFrame);
                    }
                }
            }
        }

        return frames;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
