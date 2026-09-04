using System.IO;

namespace AlliED.Helpers;

internal static class StringHelpers
{
    public static string RemoveWhitespaces(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return string.Empty;
        }

        return string.Join(string.Empty, str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
    }

    public static byte[] StringToByteArray(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            return Array.Empty<byte>();
        }

        hex = RemoveWhitespaces(hex);

        if (hex.Length % 2 == 1)
        {
            throw new InvalidDataException(nameof(hex));
        }

        byte[] bytes = new byte[hex.Length / 2];

        for (int i = 0; i < hex.Length / 2; i++)
        {
            bytes[i] = (byte)((GetHexValue(hex[i * 2]) << 4) + GetHexValue(hex[i * 2 + 1]));
        }

        return bytes;
    }

    private static int GetHexValue(char hex)
    {
        int value = hex;

        //value = value - (value < ('9' + 1) ? '0' : (value < 'a' ? ('A' - 10) : ('a' - 10)));
        return value - (value < 58 ? 48 : (value < 97 ? 55 : 87));
    }
}
