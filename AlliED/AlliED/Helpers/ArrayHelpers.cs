namespace AlliED.Helpers;

internal static class ArrayHelpers
{
    public static T[] CreateArray<T>(int count) where T : new()
    {
        var array = new T[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = new T();
        }
        return array;
    }

    public static T[] CreateArray<T>(int count, Func<T> fct)
    {
        var array = new T[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = fct();
        }
        return array;
    }

    public static void ClearArray<T>(T[] array) where T : new()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new T();
        }
    }
}
