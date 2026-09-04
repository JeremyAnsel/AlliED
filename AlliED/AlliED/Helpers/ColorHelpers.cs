using System.Windows.Media;

namespace AlliED.Helpers;

internal static class ColorHelpers
{
    public static uint FromColor(Color? color)
    {
        if (!color.HasValue)
        {
            return 0;
        }

        Color v = color.Value;
        return 0xff000000 | ((uint)v.B << 16) | ((uint)v.G << 8) | v.R;
    }

    public static Color FromUInt32(uint color)
    {
        byte[] bytes = BitConverter.GetBytes(color);
        return Color.FromRgb(bytes[0], bytes[1], bytes[2]);
    }

    public static uint FromUInt32Invert(uint color)
    {
        byte[] bytes = BitConverter.GetBytes(color);
        //return Color.FromArgb(255, bytes[2], bytes[1], bytes[0]);
        return 0xff000000 | ((uint)bytes[0] << 16) | ((uint)bytes[1] << 8) | bytes[2];
    }

    public static Brush CreateBrush(uint color)
    {
        if (color >= 0x80000000U)
        {
            color = NativeMethods.GetSysColor(color & 0xFFFFU);
        }

        var brush = new SolidColorBrush(FromUInt32(color));
        return brush;
    }
}
