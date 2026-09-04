using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AlliED.Extensions;

internal static class BitmapContextExtensions
{
    public static void DrawImage(this BitmapContext context, TRect dstRect, TBitmap srcBitmap, TRect srcRect, uint filter = 0xffffff)
    {
        int srcLeft = Math.Max(0, Math.Min(srcRect.Left, srcBitmap.Width));
        int srcTop = Math.Max(0, Math.Min(srcRect.Top, srcBitmap.Height));
        int srcRight = Math.Min(srcBitmap.Width, Math.Max(srcRect.Right, 0));
        int srcBottom = Math.Min(srcBitmap.Height, Math.Max(srcRect.Bottom, 0));

        if (srcLeft >= srcRight || srcTop >= srcBottom)
        {
            return;
        }

        srcRect = new TRect(srcLeft, srcTop, srcRight, srcBottom);

        BitmapSource bitmap = new CroppedBitmap(srcBitmap.Source, srcRect.ToInt32Rect());
        //context.DrawImage(bitmap, dstRect.ToRect());
        bitmap = CreateBitmapFilter(bitmap, filter);
        //context.DrawImage(bitmapFilter, dstRect.ToRect());

        byte[] buffer = new byte[bitmap.PixelWidth * 4 * bitmap.PixelHeight];
        bitmap.CopyPixels(buffer, bitmap.PixelWidth * 4, 0);

        var source = BitmapFactory.New(bitmap.PixelWidth, bitmap.PixelHeight).FromByteArray(buffer);
        context.WriteableBitmap.Blit(dstRect.ToRect(), source, new Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight), WriteableBitmapExtensions.BlendMode.Alpha);
    }

    private static BitmapSource CreateBitmapFilter(BitmapSource bitmap, uint filter)
    {
        var convertedBitmap = new FormatConvertedBitmap();
        convertedBitmap.BeginInit();
        convertedBitmap.Source = bitmap;
        convertedBitmap.DestinationFormat = PixelFormats.Bgra32;
        convertedBitmap.EndInit();

        if (filter == 0xffffff)
        {
            return convertedBitmap;
        }

        int w = convertedBitmap.PixelWidth;
        int h = convertedBitmap.PixelHeight;
        var buffer = new byte[w * h * 4];
        convertedBitmap.CopyPixels(buffer, w * 4, 0);

        byte newB = (byte)filter;
        byte newG = (byte)(filter >> 8);
        byte newR = (byte)(filter >> 16);
        //byte newA = (byte)(filter >> 24);

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                int index = j * w * 4 + i * 4;
                byte b = buffer[index + 0];
                byte g = buffer[index + 1];
                byte r = buffer[index + 2];
                //byte a = buffer[index + 3];

                if (b <= 4 && g <= 2 && r <= 4)
                {
                    buffer[index + 3] = 0;
                }

                b = (byte)Math.Round(b * newB / 255.0);
                g = (byte)Math.Round(g * newG / 255.0);
                r = (byte)Math.Round(r * newR / 255.0);
                //a = (byte)Math.Round(a * newA / 255.0);

                buffer[index + 0] = r;
                buffer[index + 1] = g;
                buffer[index + 2] = b;
                //buffer[index + 3] = a;
            }
        }

        var bitmapSource = BitmapSource.Create(w, h, 96.0, 96.0, PixelFormats.Bgra32, null, buffer, w * 4);
        return bitmapSource;
    }

    public static void DrawImageSolid(this BitmapContext context, TRect dstRect, TBitmap srcBitmap, TRect srcRect, uint filter)
    {
        int srcLeft = Math.Max(0, Math.Min(srcRect.Left, srcBitmap.Width));
        int srcTop = Math.Max(0, Math.Min(srcRect.Top, srcBitmap.Height));
        int srcRight = Math.Min(srcBitmap.Width, Math.Max(srcRect.Right, 0));
        int srcBottom = Math.Min(srcBitmap.Height, Math.Max(srcRect.Bottom, 0));

        if (srcLeft >= srcRight || srcTop >= srcBottom)
        {
            return;
        }

        srcRect = new TRect(srcLeft, srcTop, srcRight, srcBottom);

        BitmapSource bitmap = new CroppedBitmap(srcBitmap.Source, srcRect.ToInt32Rect());
        bitmap = CreateBitmapFilter(bitmap, filter);

        byte[] buffer = new byte[bitmap.PixelWidth * 4 * bitmap.PixelHeight];
        bitmap.CopyPixels(buffer, bitmap.PixelWidth * 4, 0);

        if (dstRect.Right < 0)
        {
            dstRect.Right = dstRect.Left;
            dstRect.Left = 0;
            MirrorX(buffer, bitmap.PixelWidth, bitmap.PixelHeight);
        }

        if (dstRect.Bottom < 0)
        {
            dstRect.Bottom = dstRect.Top;
            dstRect.Top = 0;
            MirrorY(buffer, bitmap.PixelWidth, bitmap.PixelHeight);
        }

        var source = BitmapFactory.New(bitmap.PixelWidth, bitmap.PixelHeight).FromByteArray(buffer);
        context.WriteableBitmap.Blit(dstRect.ToRect(), source, new Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight), WriteableBitmapExtensions.BlendMode.None);
    }

    private static void MirrorX(byte[] buffer, int width, int height)
    {
        int w = width / 2;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int dst = y * width * 4 + x * 4;
                int src = y * width * 4 + (width - 1 - x) * 4;

                for (int i = 0; i < 4; i++)
                {
                    byte b = buffer[dst + i];
                    buffer[dst + i] = buffer[src + i];
                    buffer[src + i] = b;
                }
            }
        }
    }

    private static void MirrorY(byte[] buffer, int width, int height)
    {
        int h = height / 2;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int dst = y * width * 4 + x * 4;
                int src = (height - 1 - y) * width * 4 + x * 4;

                for (int i = 0; i < 4; i++)
                {
                    byte b = buffer[dst + i];
                    buffer[dst + i] = buffer[src + i];
                    buffer[src + i] = b;
                }
            }
        }
    }
}
