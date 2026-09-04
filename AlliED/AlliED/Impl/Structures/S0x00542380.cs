using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x00542380
{
    public const int Size = 0x006A;

    /* 0x0000 */
    public short m000000;
    /* 0xF000002 */
    public byte[] unk000002 = new byte[100];
    /* 0x0066 */
    public short m000066;
    /* 0x0068 */
    public short m000068;

    public static S0x00542380 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x00542380();
        obj.m000000 = BitConverter.ToInt16(array, 0x00);
        array.ReadUnknown(0x02, obj.unk000002);
        obj.m000066 = BitConverter.ToInt16(array, 0x66);
        obj.m000068 = BitConverter.ToInt16(array, 0x68);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        array.WriteUnknown(0x02, unk000002);
        BitConverter.GetBytes(m000066).CopyTo(array, 0x66);
        BitConverter.GetBytes(m000068).CopyTo(array, 0x68);
        return array;
    }
}
