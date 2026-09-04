using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CD4C4
{
    public const int Size = 0x00A2;

    /* 0x0000 */
    public short m000000;
    /* 0x0002 */
    public short m000002;
    /* 0x0004 */
    public byte[] m000004 = new byte[94];
    /* 0x0062 */
    public byte m000062;
    /* 0x0063 */
    public byte m000063;
    /* 0x0064 */
    public byte m000064;
    /* 0x0065 */
    public byte m000065;
    /* 0x0066 */
    public byte[] m000066 = new byte[60];

    public static S0x004CD4C4 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004CD4C4();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        obj.m000002 = BitConverter.ToInt16(array, 0x02);
        obj.m000004 = array.Subarray(0x04, 94);
        obj.m000062 = array[0x62];
        obj.m000063 = array[0x63];
        obj.m000064 = array[0x64];
        obj.m000065 = array[0x65];
        obj.m000066 = array.Subarray(0x66, 60);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000002).CopyTo(array, 0x02);
        m000004.CopyTo(array, 0x04);
        array[0x62] = m000062;
        array[0x63] = m000063;
        array[0x64] = m000064;
        array[0x65] = m000065;
        m000066.CopyTo(array, 0x66);
        return array;
    }
}
