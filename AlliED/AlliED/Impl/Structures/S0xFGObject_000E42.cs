using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xFGObject_000E42
{
    public const int Size = 0x0600;

    /* 0x0000 */
    public S0xFGObject_000E42_000000[] M000000 = ArrayHelpers.CreateArray<S0xFGObject_000E42_000000>(8);

    public static S0xFGObject_000E42 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0xFGObject_000E42();
        for (int i = 0; i < 8; i++)
        {
            obj.M000000[i] = S0xFGObject_000E42_000000.FromByteArray(array.Subarray(i * 0xC0, S0xFGObject_000E42_000000.Size));
        }
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        for (int i = 0; i < 8; i++)
        {
            M000000[i].ToByteArray().CopyTo(array, i * 0xC0);
        }
        return array;
    }
}
