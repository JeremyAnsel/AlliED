using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004C67C0
{
    public const int Size = 0x001C;

    /* 0x0000 */
    public uint m000000;
    /* 0x0004 */
    public uint m000004;
    /* 0xF000008 */
    public byte[] unk000008 = new byte[17];
    /* 0x0019 */
    public byte m000019;
    /* 0xF00001A */
    public byte[] unk00001A = new byte[2];

    public static S0x004C67C0 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var obj = new S0x004C67C0();
        obj.m000000 = BitConverter.ToUInt32(array, 0x00);
        obj.m000004 = BitConverter.ToUInt32(array, 0x04);
        obj.unk000008 = array.Subarray(0x08, 17);
        obj.m000019 = array[0x19];
        obj.unk00001A = array.Subarray(0x1A, 2);
        return obj;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(m000000).CopyTo(array, 0x00);
        BitConverter.GetBytes(m000004).CopyTo(array, 0x04);
        unk000008.CopyTo(array, 0x08);
        array[0x19] = m000019;
        unk00001A.CopyTo(array, 0x1A);
        return array;
    }
}
