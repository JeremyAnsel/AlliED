namespace AlliED.Extensions;

internal static class StringExtensions
{
    public static string WithMaxLength(this string str, int maxLength)
    {
        if (str.Length <= maxLength)
        {
            return str;
        }

        return str[..maxLength];
    }
}
