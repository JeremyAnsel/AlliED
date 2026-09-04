namespace AlliED.Impl.Structures;

internal class S0x004CE838
{
    public const int Size = 0x000B;

    /* 0x0000 */
    public uint m000000;
    /* 0x0004 */
    public uint m000004;
    /* 0x0008 */
    public byte m000008;
    /* 0x0009 */
    public byte m000009;
    /* 0x000A */
    public bool m00000A;

    public static S0x004CE838 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x004CE838();
        buffer.m000000 = BitConverter.ToUInt32(array, 0x00);
        buffer.m000004 = BitConverter.ToUInt32(array, 0x04);
        buffer.m000008 = array[0x08];
        buffer.m000009 = array[0x09];
        buffer.m00000A = array[0x0A] != 0;
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000004).CopyTo(array, 0x04);
        array[0x08] = m000008;
        array[0x09] = m000009;
        array[0x0A] = m00000A ? (byte)1 : (byte)0;
        return array;
    }
}
