namespace AlliED.Impl.Structures;

internal class S0xFGObject_00147C
{
    public const int Size = 0x0018;

    /* 0x0000 */
    public short[] M000000 = new short[4];
    /* 0x0008 */
    public short[] M000008 = new short[4];
    /* 0x0010 */
    public short[] M000010 = new short[4];

    public short GetValue(int index)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (index < 4)
        {
            return M000000[index];
        }

        if (index < 8)
        {
            return M000008[index - 4];
        }

        if (index < 12)
        {
            return M000010[index - 8];
        }

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    public void SetValue(int index, short value)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if (index < 4)
        {
            M000000[index] = value;
            return;
        }

        if (index < 8)
        {
            M000008[index - 4] = value;
            return;
        }

        if (index < 12)
        {
            M000010[index - 8] = value;
            return;
        }

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    public static S0xFGObject_00147C FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0xFGObject_00147C();
        for (int i = 0; i < 4; i++)
        {
            obj.M000000[i] = BitConverter.ToInt16(array, 0x00 + i * 0x02);
        }
        for (int i = 0; i < 4; i++)
        {
            obj.M000008[i] = BitConverter.ToInt16(array, 0x08 + i * 0x02);
        }
        for (int i = 0; i < 4; i++)
        {
            obj.M000010[i] = BitConverter.ToInt16(array, 0x10 + i * 0x02);
        }
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        for (int i = 0; i < 4; i++)
        {
            BitConverter.GetBytes(M000000[i]).CopyTo(array, 0x00 + i * 0x02);
        }
        for (int i = 0; i < 4; i++)
        {
            BitConverter.GetBytes(M000008[i]).CopyTo(array, 0x08 + i * 0x02);
        }
        for (int i = 0; i < 4; i++)
        {
            BitConverter.GetBytes(M000010[i]).CopyTo(array, 0x10 + i * 0x02);
        }
        return array;
    }
}
