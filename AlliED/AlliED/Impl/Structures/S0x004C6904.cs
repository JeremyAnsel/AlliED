namespace AlliED.Impl.Structures;

internal class S0x004C6904
{
    public const int Size = 0x032A;

    /* 0x0000 */
    public short m000000;
    /* 0x0002 */
    public short m000002;
    /* 0x0004 */
    public short m000004;
    /* 0x0006 */
    public short m000006;
    /* 0x0008 */
    public short m000008;
    /* 0x000A */
    public short[] m00000A = new short[400];

    public static S0x004C6904 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004C6904();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        obj.m000002 = BitConverter.ToInt16(array, 0x02);
        obj.m000004 = BitConverter.ToInt16(array, 0x04);
        obj.m000006 = BitConverter.ToInt16(array, 0x06);
        obj.m000008 = BitConverter.ToInt16(array, 0x08);
        for (int i = 0; i < 400; i++)
        {
            obj.m00000A[i] = BitConverter.ToInt16(array, 0x0A + i * 0x02);
        }
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000002).CopyTo(array, 0x02);
        BitConverter.GetBytes(m000004).CopyTo(array, 0x04);
        BitConverter.GetBytes(m000006).CopyTo(array, 0x06);
        BitConverter.GetBytes(m000008).CopyTo(array, 0x08);
        for (int i = 0; i < 400; i++)
        {
            BitConverter.GetBytes(m00000A[i]).CopyTo(array, 0x0A + i * 0x02);
        }
        return array;
    }
}
