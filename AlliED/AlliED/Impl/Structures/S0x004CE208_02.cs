using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CE208_02
{
    public const int Size = 0x002A;

    /* 0x0000 */
    public S0x004CE208_02_00 m000000 = new();
    /* 0x0028 */
    public byte m000028;
    /* 0x0029 */
    public byte m000029;

    public static S0x004CE208_02 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CE208_02();
        obj.m000000 = S0x004CE208_02_00.FromByteArray(array.Subarray(0x00, 0x28));
        obj.m000028 = array[0x28];
        obj.m000029 = array[0x29];
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        m000000.ToByteArray().CopyTo(array, 0x00);
        array[0x28] = m000028;
        array[0x29] = m000029;
        return array;
    }
}
