namespace AlliED.Extensions;

internal static class ArrayExtensions
{
    public static void Clear<T>(this T[] array) where T : unmanaged
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default;
        }
    }
}
