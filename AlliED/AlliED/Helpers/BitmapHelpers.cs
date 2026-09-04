using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace AlliED.Helpers;

internal static class BitmapHelpers
{
    public static System.Windows.Media.Imaging.BitmapFrame CreateBitmapFrame(Stream ms)
    {
        using Bitmap msBitmap = (Bitmap)Image.FromStream(ms);

        var rect = new Rectangle(Point.Empty, msBitmap.Size);
        using Bitmap bitmap = msBitmap.Clone(rect, PixelFormat.Format32bppArgb);

        BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
        var bytes = new byte[bitmap.Width * bitmap.Height * 4];

        try
        {
            for (int y = 0; y < bitmapData.Height; y++)
            {
                for (int x = 0; x < bitmapData.Width; x++)
                {
                    int pos = y * bitmapData.Stride + x * 4;
                    byte b = Marshal.ReadByte(bitmapData.Scan0, pos);
                    byte g = Marshal.ReadByte(bitmapData.Scan0, pos + 1);
                    byte r = Marshal.ReadByte(bitmapData.Scan0, pos + 2);
                    bool isTransparent = b == 0 && g == 0 && r == 0;
                    byte a = isTransparent ? (byte)0 : Marshal.ReadByte(bitmapData.Scan0, pos + 3);

                    int bytesPos = (y * bitmapData.Width + x) * 4;
                    bytes[bytesPos + 0] = b;
                    bytes[bytesPos + 1] = g;
                    bytes[bytesPos + 2] = r;
                    bytes[bytesPos + 3] = a;
                }
            }
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }

        var source = System.Windows.Media.Imaging.BitmapSource.Create(bitmap.Width, bitmap.Height, 96.0, 96.0, System.Windows.Media.PixelFormats.Bgra32, null, bytes, bitmap.Width * 4);
        var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);

        return frame;
    }

    public static System.Windows.Media.Imaging.BitmapFrame CreateBitmapFrame(Stream msColor, Stream msTransparent)
    {
        using Bitmap msBitmapColor = (Bitmap)Image.FromStream(msColor);
        using Bitmap msBitmapTransparent = (Bitmap)Image.FromStream(msTransparent);

        var rectColor = new Rectangle(Point.Empty, msBitmapColor.Size);
        using Bitmap bitmapColor = msBitmapColor.Clone(rectColor, PixelFormat.Format32bppArgb);

        var rectTransparent = new Rectangle(Point.Empty, msBitmapTransparent.Size);
        using Bitmap bitmapTransparent = msBitmapTransparent.Clone(rectTransparent, PixelFormat.Format8bppIndexed);

        var bytes = new byte[bitmapColor.Width * bitmapColor.Height * 4];

        BitmapData? bitmapDataColor = null;
        BitmapData? bitmapDataTransparent = null;

        try
        {
            bitmapDataColor = bitmapColor.LockBits(rectColor, ImageLockMode.ReadOnly, bitmapColor.PixelFormat);
            bitmapDataTransparent = bitmapTransparent.LockBits(rectTransparent, ImageLockMode.ReadOnly, bitmapTransparent.PixelFormat);

            for (int y = 0; y < bitmapDataColor.Height; y++)
            {
                for (int x = 0; x < bitmapDataColor.Width; x++)
                {
                    int positionColor = y * bitmapDataColor.Stride + x * 4;
                    int positionTransparent = y * bitmapDataTransparent.Stride + x;
                    byte b = Marshal.ReadByte(bitmapDataColor.Scan0, positionColor);
                    byte g = Marshal.ReadByte(bitmapDataColor.Scan0, positionColor + 1);
                    byte r = Marshal.ReadByte(bitmapDataColor.Scan0, positionColor + 2);
                    byte a = Marshal.ReadByte(bitmapDataTransparent.Scan0, positionTransparent) == 0 ? (byte)0xff : (byte)0;

                    int bytesPos = (y * bitmapDataColor.Width + x) * 4;
                    bytes[bytesPos + 0] = b;
                    bytes[bytesPos + 1] = g;
                    bytes[bytesPos + 2] = r;
                    bytes[bytesPos + 3] = a;
                }
            }
        }
        finally
        {
            if (bitmapDataColor != null)
            {
                bitmapColor.UnlockBits(bitmapDataColor);
            }

            if (bitmapDataTransparent != null)
            {
                bitmapTransparent.UnlockBits(bitmapDataTransparent);
            }
        }

        var source = System.Windows.Media.Imaging.BitmapSource.Create(bitmapColor.Width, bitmapColor.Height, 96.0, 96.0, System.Windows.Media.PixelFormats.Bgra32, null, bytes, bitmapColor.Width * 4);
        var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);

        return frame;
    }
}
