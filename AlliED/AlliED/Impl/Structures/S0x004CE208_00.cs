using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0x004CE208_00
{
    public const int Size = 0x0080;

    /* 0x0000 */
    public short m000000;
    /* 0x0002 */
    public S0x004CE208_02[] m000002 = ArrayHelpers.CreateArray<S0x004CE208_02>(3);

    public static S0x004CE208_00 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CE208_00();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        for (int i = 0; i < 3; i++)
        {
            obj.m000002[i] = S0x004CE208_02.FromByteArray(array.Subarray(0x02 + i * 0x2A, 0x2A));
        }
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        for (int i = 0; i < 3; i++)
        {
            m000002[i].ToByteArray().CopyTo(array, 0x02 + i * 0x2A);
        }
        return array;
    }
}
