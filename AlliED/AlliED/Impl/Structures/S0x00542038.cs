namespace AlliED.Impl.Structures;

internal class S0x00542038
{
    public const int Size = 0x0006;

    /* 0x0000 */
    public short m000000;
    /* 0x0002 */
    public short m000002;
    /* 0x0004 */
    public short m000004;

    public static S0x00542038 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x00542038();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        obj.m000002 = BitConverter.ToInt16(array, 0x02);
        obj.m000004 = BitConverter.ToInt16(array, 0x04);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000002).CopyTo(array, 0x02);
        BitConverter.GetBytes(m000004).CopyTo(array, 0x04);
        return array;
    }
}
