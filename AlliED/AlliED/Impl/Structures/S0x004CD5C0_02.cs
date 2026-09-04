namespace AlliED.Impl.Structures;

internal class S0x004CD5C0_02
{
    public const int Size = 0x002C;

    /* 0x0000 */
    public short[] m000000 = new short[4];
    /* 0x0008 */
    public short[] m000008 = new short[8];
    /* 0x0018 */
    public short m000018;
    /* 0x001A */
    public short m00001A;
    /* 0x001C */
    public short[] m00001C = new short[8];

    public static S0x004CD5C0_02 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x004CD5C0_02();
        for (int i = 0; i < 4; i++)
        {
            buffer.m000000[i] = BitConverter.ToInt16(array, 0x00 + i * 0x02);
        }
        for (int i = 0; i < 8; i++)
        {
            buffer.m000008[i] = BitConverter.ToInt16(array, 0x08 + i * 0x02);
        }
        buffer.m000018 = BitConverter.ToInt16(array, 0x18);
        buffer.m00001A = BitConverter.ToInt16(array, 0x1A);
        for (int i = 0; i < 8; i++)
        {
            buffer.m00001C[i] = BitConverter.ToInt16(array, 0x1C + i * 0x02);
        }
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        for (int i = 0; i < 4; i++)
        {
            BitConverter.GetBytes(m000000[i]).CopyTo(array, 0x00 + i * 0x02);
        }
        for (int i = 0; i < 8; i++)
        {
            BitConverter.GetBytes(m000008[i]).CopyTo(array, 0x08 + i * 0x02);
        }
        BitConverter.GetBytes(m000018).CopyTo(array, 0x18);
        BitConverter.GetBytes(m00001A).CopyTo(array, 0x1A);
        for (int i = 0; i < 8; i++)
        {
            BitConverter.GetBytes(m00001C[i]).CopyTo(array, 0x1C + i * 0x02);
        }
        return array;
    }
}
